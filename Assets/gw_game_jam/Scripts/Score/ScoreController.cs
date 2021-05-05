using UniRx;
using UnityEngine;

namespace gw_game_jam.Scripts.Score
{
    public class ScoreController : MonoBehaviour
    {
        public readonly Subject<int> scoreSubject = new Subject<int>();

        public static ScoreController instance;

        [SerializeField] private int currentScore;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            scoreSubject.Subscribe(point => currentScore += point);
        }
    }
}