using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Persistence
{
    public class InvoiceRepositoryEF : IInvoiceRepository
    {
        private readonly TheaterContext _context;

        public InvoiceRepositoryEF(TheaterContext context)
        {
            _context = context;
        }

        public async Task SaveStatementAsync(Invoice invoice, Dictionary<string, Play> plays)
        {
            foreach (var playEntry in plays)
            {
                var playFromDb = await _context.Plays.FirstOrDefaultAsync(p => p.Name == playEntry.Value.Name);
                if (playFromDb == null)
                {
                    _context.Plays.Add(playEntry.Value);
                }
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
