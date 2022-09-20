using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ServerApp
{
    internal class HostedClientService : IHostedService
    {
        private readonly IConfiguration _configuration;


        public HostedClientService(IConfiguration configuration)
        {
            _configuration = configuration;
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


                if (connection.State == HubConnectionState.Disconnected)
                {
                    connection.On<string>("Connected", (string messageContent) =>
                    {
                        Console.WriteLine(messageContent);
                    });

                    await connection.StartAsync().ContinueWith(task =>
                     {
                         if (task.IsFaulted)
                         {
                             Console.WriteLine("There was an error opening the connection:{0}",
                                                 task.Exception.GetBaseException());
                         }


                         ConsoleKeyInfo cki;
                         Console.TreatControlCAsInput = true;
                         Console.WriteLine("Press ESC to stop");
                         do
                         {
                             cki = Console.ReadKey();
                             var message = cki.KeyChar.ToString();


                             if (connection.State == HubConnectionState.Connected)
                             {
                                 connection.InvokeAsync("SendMessage", message).ContinueWith(_task =>
                                 {
                                     if (_task.IsFaulted)
                                     {
                                         Console.WriteLine("There was an error calling send: {0}", _task.Exception.GetBaseException());
                                     }


                                 }).GetAwaiter().GetResult();
                             }

                         } while (cki.Key != ConsoleKey.Escape);
                     });
                }
            }




        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


    }
}
