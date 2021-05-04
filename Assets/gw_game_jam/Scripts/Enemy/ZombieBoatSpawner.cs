using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace gw_game_jam.Enemy
{
    /// <summary>
    /// ゾンビボートのスポナー.
    /// </summary>
    public class ZombieBoatSpawner : MonoBehaviour
    {

        [SerializeField] private Transform startPos;
        [SerializeField] private Transform[] firstTargetPosList;
        [SerializeField] private Transform[] secondTargetPosList;
        [SerializeField] private Transform endTargetPos;

        
        
        void Start()
        {
        }
        
        
        void Update()
        {
        
        }


        private void OnDrawGizmos()
        {
            if (startPos != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(startPos.position, Vector3.one);
            }

            if (firstTargetPosList != null)
            {
                Gizmos.color = Color.green;
                foreach (var point in firstTargetPosList)
                {
                    Gizmos.DrawCube(point.position, Vector3.one);
                }
            }

            if (secondTargetPosList != null)
            {
                Gizmos.color = Color.yellow;
                foreach (var point in secondTargetPosList)
                {
                    Gizmos.DrawCube(point.position, Vector3.one);
                }
            }

            if (endTargetPos != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(endTargetPos.position, Vector3.one);
            }
        }
    }
}

