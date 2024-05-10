using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace LocalCommerce.Shared.Setup.API;

public static class DefaultLocalCommerceWebApplication
{
    public static WebApplication Create(string[] args, Action<WebApplicationBuilder>? webappBuilder = null)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddRouting(x => x.LowercaseUrls = true);
        builder.Services.AddSerializer();

        builder.Services.AddSecretManager(builder.Configuration);
        
        if (webappBuilder != null)
        {
            webappBuilder.Invoke(builder);
        }

        return builder.Build();
    }

    public static void Run(WebApplication webApp)
    {
        if (webApp.Environment.IsDevelopment())
        {
            webApp.UseSwagger();
            webApp.UseSwaggerUI();
        }

        webApp.UseHttpsRedirection();
        webApp.UseAuthorization();
        webApp.MapControllers();
        webApp.Run();
    }
}