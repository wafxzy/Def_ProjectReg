using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Swagger
{

    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(
            OpenApiOperation operation,
            OperationFilterContext context
            )
        {
            // Policy names map to scopes
            List<string> requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy)
                .ToList();

            Type controllerType = context.MethodInfo.DeclaringType;
            if (controllerType != null)
            {
                requiredScopes.AddRange(
                controllerType.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                    .Select(attr => attr.Policy)
                    .ToList()
                );
            }

            requiredScopes = requiredScopes.Distinct().ToList();

            if (!requiredScopes.Any()) return;

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            OpenApiSecurityScheme oAuthScheme = new()
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "jwt_auth" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [ oAuthScheme ] = requiredScopes.ToList()
                }
            };
        }
    }

}
