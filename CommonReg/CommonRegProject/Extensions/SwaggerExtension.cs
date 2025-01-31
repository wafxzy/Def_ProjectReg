using CommonReg.Common.Swagger;
using Microsoft.OpenApi.Models;

namespace CommonReg.API.Extensions
{

    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(
                   this IServiceCollection services
                   )
        {
            services.AddSwaggerGen(static c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Denbeaux Project", Version = "v1" });

                c.SchemaFilter<NSwagEnumExtensionSchemaFilter>();

                c.DocumentFilter<TagReOrderDocumentFilter>();


                OpenApiSecurityScheme securityDefinition = new()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Type into the textbox: {your JWT token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

    }
}