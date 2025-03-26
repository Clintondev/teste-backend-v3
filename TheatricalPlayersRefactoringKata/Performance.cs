namespace TheatricalPlayersRefactoringKata
{
    public class Performance
    {
        public int Id { get; set; }

        public string PlayId { get; set; }
        public int Audience { get; set; }

        public Performance() { }

        public Performance(string playId, int audience)
        {
            PlayId = playId;
            Audience = audience;
        }
    }
}
