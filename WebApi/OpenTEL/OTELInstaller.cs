using Grafana.OpenTelemetry;
using Microsoft.Identity.Client;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace WebApi.OpenTEL
{
    public static class OTELInstaller
    {
        public static IServiceCollection AddOpenTelObservability(this IServiceCollection services, IConfiguration configuration)
        {
            var observabilityOptions = configuration.GetSection(nameof(TelemetrySettings)).Get<TelemetrySettings>();
            if (observabilityOptions is null || string.IsNullOrEmpty(observabilityOptions.CollectorUri.AbsoluteUri) || string.IsNullOrEmpty(observabilityOptions.ServiceName))
                return services;
            services.AddOpenTelemetry()
               .AddMetrics(observabilityOptions)
               .AddTracing(observabilityOptions);
            return services;
        }
        private static OpenTelemetryBuilder AddTracing(this OpenTelemetryBuilder builder, TelemetrySettings observabilityOptions)
        {
            if (!observabilityOptions.EnabledTracing) return builder;

            builder.WithTracing(tracing =>
            {
                tracing
                    .SetErrorStatusOnException()
                    .SetSampler(new TraceIdRatioBasedSampler(observabilityOptions.SamplingRatio))
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                    });
                tracing.AddHttpClientInstrumentation();
                tracing.UseGrafana();
                tracing.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: observabilityOptions.ServiceName));
                tracing.AddOtlpExporter(options =>
                    {
                        options.Endpoint = observabilityOptions.CollectorUri;
                        options.ExportProcessorType = ExportProcessorType.Batch;
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    });
            });

            return builder;
        }
        private static OpenTelemetryBuilder AddMetrics(this OpenTelemetryBuilder builder, TelemetrySettings telemetryOptions)
        {
            if (!telemetryOptions.EnabledMetrics) return builder;

            builder.WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                metrics.AddRuntimeInstrumentation();
                metrics.AddProcessInstrumentation();
                metrics.UseGrafana();
                metrics.AddMeter(
                            "Microsoft.AspNetCore.Hosting",
                            "Microsoft.AspnetCore.Server.Kestrel", 
                            "System.Net.Http");
                metrics.AddOtlpExporter(options =>
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
