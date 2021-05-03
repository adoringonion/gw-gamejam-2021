using System.Collections;
using System.Collections.Generic;
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
        // お試し.
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        
        
        private NavMeshAgent agent;
  
        
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(endPos.position);
        }

        
        void Update()
        {
        
        }
    }
}
