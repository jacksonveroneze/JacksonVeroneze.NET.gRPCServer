using JacksonVeroneze.NET.GRPCServer.Api.Interceptors;
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

            var serviceProvider = builder.Services
                .BuildServiceProvider();

            var appConfiguration = serviceProvider
                .GetRequiredService<AppConfiguration>();

            builder.ConfigureDefaultServices(appConfiguration);

            builder.AddLogger(appConfiguration);

            return builder;
        }

        private WebApplicationBuilder ConfigureDefaultServices(
            AppConfiguration appConfiguration)
        {
            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.Interceptors.Add<GrpcExceptionInterceptor>();
                options.Interceptors.Add<GrpcValidationInterceptor>();
            });
            
            builder.Services
                .AddAuthentication(appConfiguration)
                .AddAuthorization(appConfiguration)
                .AddCultureConfiguration()
                .AddRouting()
                .AddCorrelation()
                .AddApplicationServices()
                .AddFluentValidation(AssemblyReference.Assembly)
                .AddMapper(AssemblyReference.Assembly)
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
