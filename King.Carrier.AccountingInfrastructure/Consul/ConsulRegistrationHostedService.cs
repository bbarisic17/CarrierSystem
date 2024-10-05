using Consul;
using Microsoft.Extensions.Hosting;

namespace King.Carrier.AccountingInfrastructure.Consul;

public class ConsulRegistrationHostedService : IHostedService
{
    private readonly IConsulClient _consulClient;
    private string? RegistrationId { get; set; }

    public ConsulRegistrationHostedService(IConsulClient consulClient)
    {
        _consulClient=consulClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var serviceName = Environment.GetEnvironmentVariable("SERVICE_NAME") ?? "accountingapi";
        var servicePort = int.Parse(Environment.GetEnvironmentVariable("SERVICE_PORT") ?? "8080");
        var serviceAddress = Environment.GetEnvironmentVariable("SERVICE_ADDRESS") ?? "localhost";

        var port = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");

        var registration = new AgentServiceRegistration()
        {
            //ID = Guid.NewGuid().ToString(),
            //ID = serviceName,
            Name = serviceName,
            Address = serviceAddress,
            Port = servicePort,
            Check = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                Interval = TimeSpan.FromSeconds(15),
                HTTP = $"http://{serviceAddress}:{servicePort}/api/weatherforecast/healthcheck",
                Timeout = TimeSpan.FromSeconds(5)
            }
        };

        RegistrationId = registration.ID;

        await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _consulClient.Agent.ServiceDeregister(RegistrationId, cancellationToken);
    }
}
