using Microsoft.AspNetCore.SignalR.Client;

namespace SingnalRServiceClient
{
    public class SignalrClient : IDisposable
    {

        private HubConnection _connection = null;
        private CancellationTokenSource _source = null;


        public SignalrClient()
        {
            _source = new();
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8080/ServiceHub")
                .WithAutomaticReconnect()
                .Build();
            _connection.Closed += Closed;
        }

        public void CreateConnection(Action<HubConnection, CancellationTokenSource> action)
        {

            if (!_source.Token.IsCancellationRequested)
            {
                try
                {
                    action(_connection, _source);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ConsoleColor.Red);
                }
            }
        }


        public async Task StartConnection()
        {
            await _connection.StartAsync(_source.Token).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                        task.Exception.GetBaseException());
                }
            });

        }

        private async Task Closed(Exception error)
        {
            if (error is not null)
            {
                Console.WriteLine(error.Message, ConsoleColor.Red);
            }
            await Task.FromResult(1);
        }

        private async Task Stop()
        {
            await _connection.StopAsync(_source.Token);
        }
        private bool isDisposed = false;
        public void Dispose()
        {
            Task.WaitAll(Stop());
            _source.Cancel();
            _source.Dispose();
            _connection.DisposeAsync();
            isDisposed = true;
        }
        ~SignalrClient()
        {
            if (!isDisposed)
            {
                Dispose();
                isDisposed = true;
            }
        }
    }
}