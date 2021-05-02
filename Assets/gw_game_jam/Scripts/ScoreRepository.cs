namespace gw_game_jam.Scripts
{
    public class ScoreRepository
    {

        private Score _inmemoryScore;

        public Score InmemoryScore { get; set; }

        public ScoreRepository()
        {
            this._inmemoryScore = new Score(0);
        }

    }
}