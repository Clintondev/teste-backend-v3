using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Persistence;

namespace TheatricalPlayersRefactoringKata.AsyncProcessing
{
    public class AsyncStatementProcessor : IDisposable
    {
        private readonly ConcurrentQueue<(Invoice invoice, Dictionary<string, Play> plays)> _queue = new();
        private readonly CancellationTokenSource _cts = new();
        private readonly Task _processingTask;
        private readonly IServiceProvider _serviceProvider;

        public AsyncStatementProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _processingTask = Task.Run(ProcessQueueAsync);
        }

        public void Enqueue(Invoice invoice, Dictionary<string, Play> plays)
        {
            _queue.Enqueue((invoice, plays));
        }

        private async Task ProcessQueueAsync()
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var item))
                {
                    try
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var invoiceRepository = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();

                            await invoiceRepository.SaveStatementAsync(item.invoice, item.plays);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao processar o extrato: {ex.Message}");
                    }
                }
                else
                {
                    await Task.Delay(500, _cts.Token);
                }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            try
            {
                _processingTask.Wait();
            }
            catch (AggregateException) { }
            _cts.Dispose();
        }
    }
}
