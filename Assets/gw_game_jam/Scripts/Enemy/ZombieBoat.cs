using System;
using System.Collections;
using System.Collections.Generic;
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
        
        
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = false;

            gameObject.OnCollisionEnterAsObservable()
                .Where(collision => collision.gameObject.CompareTag("Shark"))
                .Subscribe(_ =>  Destroy(gameObject)).AddTo(this);
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
