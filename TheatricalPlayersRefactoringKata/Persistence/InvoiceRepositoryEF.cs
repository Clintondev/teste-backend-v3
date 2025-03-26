using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Persistence
{
    public class InvoiceRepositoryEF : IInvoiceRepository
    {
        private readonly TheaterContext _context;

        public InvoiceRepositoryEF(TheaterContext context)
        {
            _context = context;
        }

        public async Task SaveInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
