using UnityEngine;

namespace gw_game_jam.Scripts
{
    public class ScoreController : MonoBehaviour
    {
        private ScoreInteractor scoreInteractor;
        
        [SerializeField]
        private int currentScore;
        
        private void Awake()
        {
            scoreInteractor = new ScoreInteractor();
        }

        private void Update()
        {
            ScoreInputTest();
            currentScore = scoreInteractor.ScoreForView();
        }

        private void ScoreInputTest()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                scoreInteractor.AddScoreWhenEnemyKilled();
            }
        }
    }
}
