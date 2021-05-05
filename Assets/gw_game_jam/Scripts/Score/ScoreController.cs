using UniRx;
using UnityEngine;

namespace gw_game_jam.Scripts.Score
{
    public class ScoreController : MonoBehaviour
    {
        private Subject<int> scoreSubject;

        private static ScoreController instance;

        public ScoreController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = this;
                }

                return instance;
            }
        }

        [SerializeField] private int currentScore;

        private void Awake()
        {
            scoreSubject = new Subject<int>();
            scoreSubject.Subscribe(point => currentScore += point);
        }

        public void AddScore(int score)
        {
            scoreSubject.OnNext(score);
        }
    }
}