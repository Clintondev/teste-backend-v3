using System;

namespace TheatricalPlayersRefactoringKata.Pricing
{
    public class TragedyPriceCalculator : IPriceCalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            int lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            int amount = lines * 10;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }
            return amount;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
