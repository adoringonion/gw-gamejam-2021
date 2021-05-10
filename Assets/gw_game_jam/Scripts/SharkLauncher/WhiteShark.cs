using System;
using gw_game_jam.Scripts.Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class WhiteShark : MonoBehaviour, IPlayerBullet
    {
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
                    Destroy(gameObject);
                }).AddTo(this);
            
            Observable.Interval(TimeSpan.FromSeconds(3)).Subscribe(_ =>
                Destroy(gameObject)
            ).AddTo(this);
        }

        
        public void AddForce(Vector3 vec)
        {
            rigidBody.AddForce(vec, ForceMode.Impulse);
        }
    }
}