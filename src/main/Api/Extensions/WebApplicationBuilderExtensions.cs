using JacksonVeroneze.NET.GRPCServer.Api.Grpc.Interceptors;
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
            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = builder.Environment.IsDevelopment();
                options.Interceptors.Add<GrpcExceptionInterceptor>();
                options.Interceptors.Add<GrpcValidationInterceptor>();
            });
            
            builder.Services
                .AddProblemDetails()
                .AddExceptionHandler<CustomExceptionHandler>()
                .AddAuthentication(appConfiguration)
                .AddAuthorization(appConfiguration)
                .AddJsonOptionsSerialize()
                .AddHttpContextAccessor()
                .AddCultureConfiguration()
                .AddAppVersioning()
                .AddRouting()
                .AddCorrelation()
                .AddApplicationServices()
                .AddFluentValidation(AssemblyReference.Assembly)
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
