using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using gw_game_jam.Scripts.Score;
using gw_game_jam.Scripts.SharkLauncher;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;


namespace gw_game_jam.Enemy
{
    /// <summary>
    /// ゾンビのボート.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieBoat : MonoBehaviour
    {
        private const float NextTargetChangeDistance = 3f;
        private const int HealthValue = 30;
        private const int Score = 10;


        private Queue<Vector3> targetPositions;
        private NavMeshAgent agent;
        private ReactiveProperty<int> health;
        private ScoreController scoreController;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = false;
            health = new ReactiveProperty<int>(HealthValue);
            scoreController = gameObject.AddComponent<ScoreController>().Instance;
            
            // サメタグが付いたオブジェクトと衝突した場合のみ、ダメージ計算
            this.OnCollisionEnterAsObservable().Where(collision => collision.gameObject.CompareTag("Shark"))
                .Select(collision => collision.gameObject.GetComponent<WhiteShark>())
                .Subscribe(whiteShark => health.Value -= WhiteShark.AttackValue).AddTo(gameObject);
            // 体力変動ごとにチェック。0以下ならDestroyしてスコア加算
            health.Where(value => value <= 0).Subscribe(_ =>
            {
                scoreController.AddScore(Score);
                Destroy(gameObject);
            }).AddTo(gameObject);
        }

        
        private void Update()
        {
            if (agent.remainingDistance <= NextTargetChangeDistance)
            {
                if (0 < targetPositions.Count)
                {
                    agent.SetDestination(targetPositions.Dequeue());
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }


        public void SetTargetPositions(
            Queue<Vector3> positions)
        {
            targetPositions = positions;
            agent.enabled = true;
            agent.SetDestination(targetPositions.Dequeue());
        }
    }
}
