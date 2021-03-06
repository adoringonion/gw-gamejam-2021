using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using gw_game_jam.Scripts.Enemy;
using gw_game_jam.Scripts.Score;
using gw_game_jam.Scripts.SharkLauncher;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


namespace gw_game_jam.Enemy
{
    /// <summary>
    /// ゾンビのボート.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieBoat : MonoBehaviour, IEnemy
    {
        private const float NextTargetChangeDistance = 3f;
        private const int HealthValue = 10;
        private const int Score = 10;

        [SerializeField] private GameObject bombEffectPrefab;

        private Queue<Vector3> targetPositions;
        private NavMeshAgent agent;
        private ReactiveProperty<int> health;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = false;
            
            // Health はとりあえず 10 か 20 をランダムに.
            health = new ReactiveProperty<int>(HealthValue * UnityEngine.Random.Range(1, 3));
            
            // 体力変動ごとにチェック。0以下ならDestroyしてスコア加算
            health.Where(value => value <= 0).Subscribe(_ => Death()).AddTo(gameObject);
        }

        
        private void Update()
        {
            UpdateAgent();
        }
        

        private void UpdateAgent()
        {
            // isStopped でブロックしようとしても止まらんかったので null 代入で対応.
            if (agent == null) return;
            
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

        public void SetDamage(int damage)
        {
            health.Value -= damage;
        }

        private void Death()
        {
            // なんかと止める方法が無かった.
            agent = null;
            
            ScoreController.AddScore(Score);
            for (var i = 0; i < UnityEngine.Random.Range(1, 4); ++i)
            {
                var obj = Instantiate(bombEffectPrefab);
                obj.transform.SetPositionAndRotation(transform.position + (UnityEngine.Random.insideUnitSphere * 3f), Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
