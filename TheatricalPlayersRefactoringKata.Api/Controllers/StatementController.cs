using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.AsyncProcessing;
using TheatricalPlayersRefactoringKata.Persistence;
using Swashbuckle.AspNetCore.Filters;
using TheatricalPlayersRefactoringKata.Api.Examples;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly AsyncStatementProcessor _processor;
        private readonly TheaterContext _context;

        public StatementController(AsyncStatementProcessor processor, TheaterContext context)
        {
            _processor = processor;
            _context = context;
        }

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
            var invoices = _context.Invoices.Include(i => i.Performances).ToList();
            return Ok(invoices);
        }
    }

    public class StatementRequest
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
    }
}
