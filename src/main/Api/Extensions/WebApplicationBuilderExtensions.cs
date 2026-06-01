using JacksonVeroneze.NET.GRPCServer.Api.Middlewares;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder Configure()
        {
            builder.Services.AddAppConfigs(builder.Configuration);

            var appConfiguration = builder.Configuration
                .Get<AppConfiguration>()!;

            builder.ConfigureDefaultServices(appConfiguration);

            builder.AddLogger(appConfiguration);

            return builder;
        }

        private WebApplicationBuilder ConfigureDefaultServices(
            AppConfiguration appConfiguration)
        {
            builder.Services.AddGrpc(builder.Environment.IsDevelopment());
            builder.Services.AddMcp(appConfiguration);

            builder.Services
                .AddProblemDetails()
                .AddExceptionHandler<CustomExceptionHandler>()
                .AddAuthentication(appConfiguration)
                .AddAuthorization(appConfiguration)
                .AddJsonOptionsSerialize()
                .AddAppVersioning()
                .AddRouting()
                .AddCorrelation()
                .AddApplicationServices()
                .AddMapper(AssemblyReference.Assembly)
                .AddCached(appConfiguration)
                .AddOpenTelemetry(appConfiguration)
                .AddDatabase(appConfiguration)
                .AddHealthChecks();

            if (!builder.Environment.IsProduction())
            {
                builder.Services.AddGrpcReflection();
            }

            return builder;
        }
    }
}
