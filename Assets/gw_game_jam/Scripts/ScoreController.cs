using UnityEngine;

namespace gw_game_jam.Scripts
{
    public class ScoreController : MonoBehaviour
    {
        private ScoreInteractor _scoreInteractor;
        
        [SerializeField]
        public int currentScore;
        
        private void Awake()
        {
            _scoreInteractor = new ScoreInteractor();
        }

        private void Update()
        {
            ScoreInputTest();
            currentScore = _scoreInteractor.ScoreForView();
        }

        private void ScoreInputTest()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _scoreInteractor.addScoreWhenEnemyKilled();
            }
        }
    }
}
