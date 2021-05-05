using UniRx;
using UnityEngine;

namespace gw_game_jam.Scripts.Score
{
    public class ScoreController : MonoBehaviour
    {
        private readonly Subject<int> scoreSubject = new Subject<int>();

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
            scoreSubject.Subscribe(point => currentScore += point);
        }

        public void AddScore(int score)
        {
            scoreSubject.OnNext(score);
        }
    }
}