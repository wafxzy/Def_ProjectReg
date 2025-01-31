using CommonReg.BLL.Services.Interfaces;
using CommonReg.BLL.Services;
using CommonReg.API.Extensions;
using CommonReg.BLL.Extensions;
using CommonReg.DAL.Extensions;
using CommonReg.Common.JWTToken.Extensions;
using CommonReg.EmailSender.Extensions;
using CommonReg.Common.Helpers;
using CommonReg.Common.JWTToken.Constants;
using Microsoft.OpenApi.Models;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json");
});

//builder.Host.UseLoggingSerilog(builder.Configuration);

builder.Services.AddServices();
builder.Services.JwtTokenAddAuthentication();
builder.Services.AddSwagger();
builder.Services.AddSmtpEmailServices(builder.Configuration);
builder.Services.AddReposithories();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization(config =>
{
    string[] names = Enum.GetNames(typeof(UserPermission));
    Array values = Enum.GetValues(typeof(UserPermission));

    for (int i = 0; i < names.Length; i++)
    {
        string name = names[i];
        string val = ((int)(values.GetValue(i) ?? 0)).ToString(); // Ensure non-null value

        config.AddPolicy(name, policyConfig =>
        {
            policyConfig.RequireClaim(JWTClaims.Permission, val);
        });
    }
});var app = builder.Build();

app.UseCors(builder => builder
                   .SetIsOriginAllowed(_ => true)
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetPreflightMaxAge(TimeSpan.FromDays(1d))
           );

app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = $"https://{httpReq.Host.Value}" } };
    });
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Common Project v1");
    c.DisplayOperationId();
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.UseAuthentication();
app.MapControllers();

app.Run();
