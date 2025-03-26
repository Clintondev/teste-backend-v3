using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Pricing;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class PricingTests
    {
        [Fact]
        public void TragedyPriceCalculator_ShouldCalculateCorrectAmountAndCredits()
        {
            var calculator = new TragedyPriceCalculator();
            var play = new Play("Hamlet", 4024, "tragedy");
            var performance = new Performance("hamlet", 35);

            int amount = calculator.CalculateAmount(performance, play);
            int credits = calculator.CalculateVolumeCredits(performance, play);

            Assert.True(amount > 0);
            Assert.Equal(5, credits); 
        }

        [Fact]
        public void ComedyPriceCalculator_ShouldCalculateCorrectAmountAndCredits()
        {
            var calculator = new ComedyPriceCalculator();
            var play = new Play("As You Like It", 2670, "comedy");
            var performance = new Performance("as-like", 25);

            int amount = calculator.CalculateAmount(performance, play);
            int credits = calculator.CalculateVolumeCredits(performance, play);

            Assert.True(amount > 0);
            Assert.True(credits >= 0);
        }

        [Fact]
        public void HistoryPriceCalculator_ShouldCalculateAsSumOfTragedyAndComedy()
        {
            var calculator = new HistoryPriceCalculator();
            var play = new Play("Henry V", 3227, "history");
            var performance = new Performance("henry-v", 20);

            int amount = calculator.CalculateAmount(performance, play);
            int expectedTragedy = new TragedyPriceCalculator().CalculateAmount(performance, play);
            int expectedComedy = new ComedyPriceCalculator().CalculateAmount(performance, play);

            Assert.Equal(expectedTragedy + expectedComedy, amount);
        }
    }
}