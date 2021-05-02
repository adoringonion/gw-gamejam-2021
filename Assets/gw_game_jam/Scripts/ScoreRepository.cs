namespace gw_game_jam.Scripts
{
    public class ScoreRepository
    {
        public Score InmemoryScore { get; set; }

        public ScoreRepository()
        {
            this.InmemoryScore = new Score(0);
        }

    }
}