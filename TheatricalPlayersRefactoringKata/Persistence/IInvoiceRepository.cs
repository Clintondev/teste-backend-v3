using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Persistence
{
    public interface IInvoiceRepository
    {
        Task SaveInvoiceAsync(Invoice invoice);
    }
}
