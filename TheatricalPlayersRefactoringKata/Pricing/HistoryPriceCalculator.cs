using System;

namespace TheatricalPlayersRefactoringKata.Pricing
{
    public class HistoryPriceCalculator : IPriceCalculator
    {
        private readonly TragedyPriceCalculator tragedyCalculator = new TragedyPriceCalculator();
        private readonly ComedyPriceCalculator comedyCalculator = new ComedyPriceCalculator();

        public int CalculateAmount(Performance performance, Play play)
        {
            return tragedyCalculator.CalculateAmount(performance, play)
                 + comedyCalculator.CalculateAmount(performance, play);
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
