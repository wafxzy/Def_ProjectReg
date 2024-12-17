using CommonReg.Common.JWTToken.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;

namespace CommonReg.Common.JWTToken.Extensions
{
    public static class ServiceCollectionExtender
    {

        public static void JwtTokenAddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateIssuer = true,

                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateAudience = true,

                            RequireExpirationTime = true,
                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                            ClockSkew = TimeSpan.FromSeconds(30),
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                // if correct header present then do noting
                                if (context.Request.Headers.ContainsKey(HeaderNames.Authorization))
                                {
                                    return Task.CompletedTask;
                                }

                                // if token sent as a query parameter then use it as token
                                if (context.Request.Query.TryGetValue(AuthOptions.JWT_QUERY_PARAMETER_NAME, out StringValues jwtToken))
                                {
                                    context.Token = jwtToken.FirstOrDefault();
                                }

                                return Task.CompletedTask;
                            },
                            OnAuthenticationFailed = context =>
                            {
                                Trace.TraceError($"JWT Auth failed with message: {context.Exception?.GetType()}-{context.Exception?.Message}");
                                return Task.CompletedTask;
                            }
                        };
                    });
        }
    }
}
