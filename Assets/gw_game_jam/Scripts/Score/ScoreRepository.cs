namespace gw_game_jam.Scripts
{
    public class ScoreRepository
    {
        public Score InmemoryScore { get; set; }

        public ScoreRepository()
        {
            InmemoryScore = new Score(0);
        }

    }
}