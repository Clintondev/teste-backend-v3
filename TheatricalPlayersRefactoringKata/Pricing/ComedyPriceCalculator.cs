using System;

namespace TheatricalPlayersRefactoringKata.Pricing
{
    public class ComedyPriceCalculator : IPriceCalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            int lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            int amount = lines * 10;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
            }
            amount += 300 * performance.Audience;
            return amount;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)performance.Audience / 5);
            return credits;
        }
    }
}
