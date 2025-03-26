using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Persistence
{
    public interface IInvoiceRepository
    {
        Task SaveStatementAsync(Invoice invoice, Dictionary<string, Play> plays);
    }
}
