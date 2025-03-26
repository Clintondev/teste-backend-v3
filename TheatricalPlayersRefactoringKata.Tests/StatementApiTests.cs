using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using TheatricalPlayersRefactoringKata.Api;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public StatementApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostStatement_ReturnsAccepted()
        {
            var request = new
            {
                Invoice = new
                {
                    Customer = "BigCo",
                    Performances = new[]
                    {
                        new { PlayId = "hamlet", Audience = 55 },
                        new { PlayId = "as-like", Audience = 35 },
                        new { PlayId = "othello", Audience = 40 }
                    }
                },
                Plays = new Dictionary<string, object>
                {
                    { "hamlet", new { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
                    { "as-like", new { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
                    { "othello", new { Name = "Othello", Lines = 3560, Type = "tragedy" } }
                }
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Statement", content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }
    }
}
