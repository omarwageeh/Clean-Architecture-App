using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace WebApi.OpenTEL
{
    public static class OTELInstaller
    {
        public static IServiceCollection AddOpenTelObservability(this IServiceCollection services, IConfiguration configuration, List<string> metricsNames)
        {
            var observabilityOptions = configuration.GetSection(nameof(TelemetrySettings)).Get<TelemetrySettings>();
            if (observabilityOptions is null || string.IsNullOrEmpty(observabilityOptions.CollectorUri.AbsoluteUri) || string.IsNullOrEmpty(observabilityOptions.ServiceName))
                return services;
            services.AddOpenTelemetry()
               .ConfigureResource(resource => resource.AddService(observabilityOptions.ServiceName))
               .AddMetrics(observabilityOptions, metricsNames)
               .AddTracing(observabilityOptions);
            return services;
        }
        private static OpenTelemetryBuilder AddTracing(this OpenTelemetryBuilder builder, TelemetrySettings telemetryOptions)
        {
            if (!telemetryOptions.EnabledTracing) return builder;

            builder.WithTracing(tracing =>
            {
                tracing
                    .SetErrorStatusOnException()
                    .SetSampler(new AlwaysOnSampler())
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                    });
                tracing.AddHttpClientInstrumentation();
                tracing.UseGrafana();

                tracing
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = telemetryOptions.CollectorUri;
                        options.ExportProcessorType = ExportProcessorType.Batch;
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    });
            });

            return builder;
        }
        private static OpenTelemetryBuilder AddMetrics(this OpenTelemetryBuilder builder, TelemetrySettings telemetryOptions, List<string> metricsNames)
        {
            if (!telemetryOptions.EnabledMetrics) return builder;

            builder.WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                metrics.AddRuntimeInstrumentation();
                metrics.AddProcessInstrumentation();
                metrics.UseGrafana();
                foreach (var metric in metricsNames)
                {
                    metrics.AddMeter(metric);
                }

                metrics
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = telemetryOptions.CollectorUri;
                        options.ExportProcessorType = ExportProcessorType.Batch;
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    });
            });

            return builder;
        }


    }

}
