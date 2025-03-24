namespace TheatricalPlayersRefactoringKata.Pricing
{
    public interface IPriceCalculator
    {
        int CalculateAmount(Performance performance, Play play);

        int CalculateVolumeCredits(Performance performance, Play play);
    }
}
