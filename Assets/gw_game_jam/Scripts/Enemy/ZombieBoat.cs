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
        private const int HealthValue = 1;
        private const int Score = 10;

        [SerializeField] private GameObject bombEffectPrefab;

        private Queue<Vector3> targetPositions;
        private NavMeshAgent agent;
        private ReactiveProperty<int> health;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = false;
            health = new ReactiveProperty<int>(HealthValue);
            
            // 体力変動ごとにチェック。0以下ならDestroyしてスコア加算
            health.Where(value => value <= 0).Subscribe(_ => Death()).AddTo(gameObject);
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

        public void SetDamage(int damage)
        {
            health.Value -= damage;
        }

        private void Death()
        {
            ScoreController.AddScore(Score);

            for (var i = 0; i < UnityEngine.Random.Range(0, 4); ++i)
            {
                var obj = Instantiate(bombEffectPrefab);
                obj.transform.SetPositionAndRotation(transform.position + (UnityEngine.Random.insideUnitSphere * 3f), Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
