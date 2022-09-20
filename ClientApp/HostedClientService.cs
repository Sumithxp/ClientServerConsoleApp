using ClientApp.Extentions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ClientApp
{
    internal class HostedClientService : IHostedService
    {

        private readonly IConfiguration _configuration;

        public HostedClientService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {            
            var url = _configuration.GetSection("SignalRServiceUrl").Value;

            while (!cancellationToken.IsCancellationRequested)
            {
                var connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

                while (true)
                {

                    if (connection.State == HubConnectionState.Disconnected)
                    {
                        connection.On<string>("Connected", (string messageContent) =>
                        {
                            Console.WriteLine(messageContent);
                        }); ;
                        connection.On<string>("ReceiveMessage", (string messageContent) =>
                        {
                            ConsoleManager.WriteLine(messageContent);

                        });
                        // connection.StartAsync().GetAwaiter().GetResult();
                        await connection.StartAsync().ContinueWith(task =>
                        {
                            if (task.IsFaulted)
                            {
                                Console.WriteLine("There was an error opening the connection:{0}",
                                                    task.Exception.GetBaseException());
                            }
                        });


                        Console.ReadLine();

                    }


                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
