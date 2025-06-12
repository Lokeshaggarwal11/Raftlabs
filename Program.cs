using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaftLabs.ReqResApiClient.Services.Abstraction;
using RaftLabs.ReqResApiClient.Services.Implementation;
using RaftLabs.ReqResApiClient.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<ApiOptions>(context.Configuration.GetSection("ReqResApi"));
        services.AddHttpClient<IExternalUserService, ExternalUserService>();
    })
    .Build();

var service = host.Services.GetRequiredService<IExternalUserService>();
var users = await service.GetAllUsersAsync();
Console.WriteLine($"Fetched {users.Count()} users.");
