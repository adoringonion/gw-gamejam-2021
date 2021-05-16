using System;
using gw_game_jam.Scripts.Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class WhiteShark : MonoBehaviour, IPlayerBullet
    {

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
        }


        private void Death()
        {
            for (var i = 0; i < UnityEngine.Random.Range(0, 4); ++i)
            {
                var obj = Instantiate(bombEffectPrefab);
                obj.transform.SetPositionAndRotation(transform.position + UnityEngine.Random.insideUnitSphere, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        
        public void AddForce(Vector3 vec)
        {
            rigidBody.AddForce(vec, ForceMode.Impulse);
        }
        
        
    }
}