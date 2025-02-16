namespace WebApi.OpenTEL
{
    public class TelemetrySettings
    {
        public string ServiceName { get; set; } = default!;
        public Uri CollectorUri { get; set; } = new Uri("http://localhost:4317");
        public bool EnabledTracing { get; set; } = false;
        public bool EnabledMetrics { get; set; } = false;
        public double SamplingRatio { get; set; } = 1.0;
    }
}
