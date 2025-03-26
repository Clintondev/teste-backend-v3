using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.AsyncProcessing;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class AsyncStatementProcessorTests
    {
        [Fact]
        public async Task AsyncStatementProcessor_ShouldGenerateXmlFile()
        {
            string testDirectory = "TestExtratosXML";
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }
            Directory.CreateDirectory(testDirectory);

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") }
            };

            var invoice = new Invoice("TestCustomer", new List<Performance>
            {
                new Performance("hamlet", 35)
            });

            using (var processor = new AsyncStatementProcessor(testDirectory))
            {
                processor.Enqueue(invoice, plays);

                await Task.Delay(1000);
            }

            var files = Directory.GetFiles(testDirectory, "*.xml");
            Assert.NotEmpty(files);

            Directory.Delete(testDirectory, true);
        }
    }
}