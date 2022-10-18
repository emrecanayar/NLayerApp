using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using NLayerApp.API.Extensions;
using NLayerApp.API.Filters;
using NLayerApp.API.Middlewares;
using NLayerApp.API.Modules;
using NLayerApp.Core.Configurations;
using NLayerApp.Core.Entities;
using NLayerApp.Service.Validations;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(options => { options.Filters.Add(new ValidateFilterAttribute()); options.Filters.Add(new RequestLimitAttribute("RequestLimit")); }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductCreateRequestDtoValidator>());

builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

#region OptionPatterns
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));
#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "NlayerApp API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.ConfigureServices(builder.Configuration.GetConnectionString("SqlConnection"), builder.Configuration);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new ServiceModule()));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            options.DefaultModelsExpandDepth(-1);
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "NLayerApp Backend v1");
        });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomException();
app.UseRequestResponseLoggingMiddleware();
app.UseCors("AllowSitesAndSubDomains");
app.UseIpSafe();
app.MapControllers();

app.Run();
