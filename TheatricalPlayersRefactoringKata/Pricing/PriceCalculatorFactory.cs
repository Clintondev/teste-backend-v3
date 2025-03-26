using System;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Pricing;

namespace TheatricalPlayersRefactoringKata.Pricing
{
    public class PriceCalculatorFactory
    {
        public IPriceCalculator Create(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyPriceCalculator(),
                "comedy" => new ComedyPriceCalculator(),
                "history" => new HistoryPriceCalculator(),
                _ => throw new Exception("unknown type: " + playType),
            };
        }
    }
}