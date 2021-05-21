using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

using gw_game_jam.Scripts.Enemy;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class WhiteShark : MonoBehaviour, IPlayerBullet
    {
        private const float ToDefaultScaleTime = 1f;
        

        [SerializeField] private GameObject bombEffectPrefab;
        
        private Rigidbody rigidBody;
        private int attackValue = 10;

        private void Awake()
        {
            rigidBody = gameObject.GetComponent<Rigidbody>();
            this.OnCollisionEnterAsObservable().Where(collision => collision.gameObject.CompareTag("Enemy"))
                .Select(collision => collision.gameObject.GetComponent<IEnemy>())
                .Subscribe(enemy =>
                {
                    enemy.SetDamage(attackValue);
                    Death();
                }).AddTo(this);
            
            Observable.Interval(TimeSpan.FromSeconds(3)).Subscribe(_ =>
                Death()
            ).AddTo(this);

            ToDefaultScale().Forget();
        }


        private void Death()
        {
            for (var i = 0; i < UnityEngine.Random.Range(1, 4); ++i)
            {
                var obj = Instantiate(bombEffectPrefab);
                obj.transform.SetPositionAndRotation(transform.position + UnityEngine.Random.insideUnitSphere, Quaternion.identity);
            }
            Destroy(gameObject);
        }


         /// <summary>
         /// サメの拡大率を徐々に1.0にしていく.
         /// </summary>
         /// <returns></returns>
        private async UniTask ToDefaultScale()
        {
            float scale = 0.1f;
            float timeCount = 0f;
            transform.localScale = Vector3.one * scale;
            while (scale < 1f)
            {
                timeCount += Time.deltaTime;
                scale = timeCount / ToDefaultScaleTime; 
                transform.localScale = (scale < 1f) ? Vector3.one * scale : Vector3.one;
                await UniTask.DelayFrame(1);
            }
        }


        public void AddForce(Vector3 vec)
        {
            rigidBody.AddForce(vec, ForceMode.Impulse);
        }
        
        
    }
}