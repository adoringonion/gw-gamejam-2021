namespace gw_game_jam.Scripts
{
    public class ScoreInteractor
    {
        private readonly ScoreRepository scoreRepository;
        private const int KillPoint = 10;

        public ScoreInteractor()
        {
            scoreRepository = new ScoreRepository();
        }

        public void AddScoreWhenEnemyKilled()
        {
            scoreRepository.InmemoryScore = scoreRepository.InmemoryScore.Add(KillPoint);
        }

        public int ScoreForView()
        {
           return scoreRepository.InmemoryScore.Value;
        }
    }
}