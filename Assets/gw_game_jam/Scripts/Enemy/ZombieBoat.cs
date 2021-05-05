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


        private Queue<Vector3> targetPositions;
        private NavMeshAgent agent;
        private readonly ReactiveProperty<int> health = new ReactiveProperty<int>(30);
        private readonly ScoreController scoreController = new ScoreController().Instance;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = false;
            this.OnCollisionEnterAsObservable().Where(collision => collision.gameObject.CompareTag("Shark"))
                .Select(collision => collision.gameObject.GetComponent<WhiteShark>())
                .Subscribe(whiteShark => health.Value -= WhiteShark.AttackValue);
            health.Where(value => value <= 0).Subscribe(_ => scoreController.AddScore(10));
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
