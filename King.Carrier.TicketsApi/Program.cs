
using Consul;
using King.Carrier.TicketsApplication.Integrations.RabbitMQ.Tickets;
using King.Carrier.TicketsApplication.Integrations.TicketsApi.RabbitMq;
using King.Carrier.TicketsInfrastructure.Consul;
using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ;
using King.Carrier.TicketsInfrastructure.Integrations.RabbitMQ.Tickets;
using MassTransit;
using RabbitMQ.Client;

namespace King.Carrier.TicketsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            //builder.Services.AddMassTransit(x =>
            //{
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host(new Uri("rabbitmq://rabbitmq:5672"), h =>
            //        {
            //            h.Username("guest");
            //            h.Password("guest");
            //        });

            //        cfg.Publish<TicketMessage>(x =>
            //        {
            //            x.Exclude = true;
            //        });

            //        //cfg.Message<TicketMessage>(config =>
            //        //{
            //        //    config.SetEntityName("tickets-exchange"); // This sets the exchange name for TicketMessage
            //        //});
            //    });
            //});

            //napravit da se ovo automatski registrira
            //koristiti konfiguracije
            builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri("http://consul:8500");
            }));
            builder.Services.AddHostedService<ConsulRegistrationHostedService>();
            builder.Services.AddScoped<TicketPublisher>();
            builder.Services.AddScoped<RabbitMqSetupService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
