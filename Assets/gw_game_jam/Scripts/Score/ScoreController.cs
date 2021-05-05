using UniRx;
using UnityEngine;

namespace gw_game_jam.Scripts
{
    public class ScoreController : MonoBehaviour
    {
        private ScoreInteractor scoreInteractor;
        public Subject<Unit> scoreSubject = new Subject<Unit>();

        public static ScoreController instance;
        
        [SerializeField]
        private int currentScore;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                scoreInteractor = new ScoreInteractor();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
