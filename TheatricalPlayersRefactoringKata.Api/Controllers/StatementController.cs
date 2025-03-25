using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.AsyncProcessing;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata.Api.Examples; 

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private static readonly AsyncStatementProcessor _processor = new AsyncStatementProcessor("ExtratosXML");

        /// <summary>
        /// </summary>
        /// <param name="request">Os dados da fatura e o dicionário de peças.</param>
        /// <returns>Status de aceitação.</returns>
        [HttpPost]
        [SwaggerRequestExample(typeof(StatementRequest), typeof(StatementRequestExample))]
        public IActionResult PostStatement([FromBody] StatementRequest request)
        {
            if (request == null || request.Invoice == null || request.Plays == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _processor.Enqueue(request.Invoice, request.Plays);
            return Accepted("Extrato enfileirado para processamento.");
        }

        [HttpGet]
        public IActionResult GetStatements()
        {
            var files = System.IO.Directory.GetFiles("ExtratosXML", "*.xml");
            return Ok(files);
        }
    }

    public class StatementRequest
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
    }
}
