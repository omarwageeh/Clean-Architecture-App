using Asp.Versioning;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using WebApi.Middleware;
using WebApi.OpenTEL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddRepositories();
builder.Services.AddMyDependencyGroup();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddOpenTelObservability(builder.Configuration, []);
var telemetrySettings = builder.Configuration.GetSection(nameof(TelemetrySettings)).Get<TelemetrySettings>()!;
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .Enrich.FromLogContext()
        .WriteTo.OpenTelemetry(options =>
        {
            options.Endpoint = telemetrySettings.CollectorUri.AbsoluteUri;
            options.Protocol = OtlpProtocol.Grpc;
            options.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField | IncludedData.SourceContextAttribute;
            options.ResourceAttributes = new Dictionary<string, object>
                                                    {
                                                        {"service.name", telemetrySettings.ServiceName},
                                                    };
        })
        .WriteTo.Console();
});
//builder.Host.UseSerilog((context, cfg) =>
//cfg.ReadFrom.Configuration(context.Configuration));


builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
