using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.StatementOutput;

namespace TheatricalPlayersRefactoringKata.AsyncProcessing
{
    public class AsyncStatementProcessor : IDisposable
    {
        private readonly ConcurrentQueue<(Invoice invoice, Dictionary<string, Play> plays)> _queue = new();
        private readonly CancellationTokenSource _cts = new();
        private readonly Task _processingTask;
        
        private readonly string _outputDirectory;
        
        private readonly XmlStatementPrinter _xmlPrinter = new XmlStatementPrinter();

        public AsyncStatementProcessor(string outputDirectory)
        {
            _outputDirectory = outputDirectory;
            Directory.CreateDirectory(_outputDirectory);
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

                        var xmlContent = _xmlPrinter.Print(item.invoice, item.plays);
                        

                        var fileName = Path.Combine(_outputDirectory, $"Statement_{DateTime.Now:yyyyMMdd_HHmmssfff}.xml");
                        
                        await File.WriteAllTextAsync(fileName, xmlContent);
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
