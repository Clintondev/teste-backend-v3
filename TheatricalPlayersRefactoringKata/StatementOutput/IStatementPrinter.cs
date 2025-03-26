using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata.StatementOutput
{
    public interface IStatementPrinter
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}