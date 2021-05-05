using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class WhiteShark : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private readonly Subject<Unit> hitSubject = new Subject<Unit>();

        public IObservable<Unit> OnHitAsync => hitSubject;

        
        private void Awake()
        {
            rigidBody = gameObject.GetComponent<Rigidbody>();
        }

        
        private void Start()
        {
            gameObject.OnCollisionEnterAsObservable().Where(collision => collision.gameObject.CompareTag("ZombieBoat"))
                .Subscribe(collision =>
                {
                    hitSubject.OnNext(Unit.Default);
                    Destroy(collision.gameObject);
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

        
        private void OnDestroy()
        {
           hitSubject.Dispose(); 
        }
    }
}