using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Pricing;
using TheatricalPlayersRefactoringKata.StatementOutput;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter : IStatementPrinter
    {
        private readonly PriceCalculatorFactory _calculatorFactory = new PriceCalculatorFactory();

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            int totalAmount = 0;
            int volumeCredits = 0;
            string result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculatorFactory.Create(play.Type);

                int thisAmount = calculator.CalculateAmount(perf, play);
                int perfCredits = calculator.CalculateVolumeCredits(perf, play);

                volumeCredits += perfCredits;

                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                    play.Name, thisAmount / 100m, perf.Audience);
                totalAmount += thisAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}