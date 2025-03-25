using System.Collections.Generic;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Api.Controllers;

namespace TheatricalPlayersRefactoringKata.Api.Examples
{
    public class StatementRequestExample : IExamplesProvider<StatementRequest>
    {
        public StatementRequest GetExamples()
        {
            return new StatementRequest
            {
                Invoice = new Invoice("BigCo", new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40)
                }),
                Plays = new Dictionary<string, Play>
                {
                    { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                    { "as-like", new Play("As You Like It", 2670, "comedy") },
                    { "othello", new Play("Othello", 3560, "tragedy") }
                }
            };
        }
    }
}
