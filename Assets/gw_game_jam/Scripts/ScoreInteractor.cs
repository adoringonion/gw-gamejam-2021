namespace gw_game_jam.Scripts
{
    public class ScoreInteractor
    {
        private ScoreRepository _scoreRepository;
        private const int KillPoint = 10;

        public ScoreInteractor()
        {
            _scoreRepository = new ScoreRepository();
        }

        public void addScoreWhenEnemyKilled()
        {
            _scoreRepository.InmemoryScore = _scoreRepository.InmemoryScore.add(KillPoint);
        }

        public int ScoreForView()
        {
           return _scoreRepository.InmemoryScore.Value;
        }
    }
}