using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace gw_game_jam.Enemy
{
    /// <summary>
    /// ゾンビボートのスポナー.
    /// </summary>
    public class ZombieBoatSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject zombieBoatPrefab;
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform[] firstTargetPosList;
        [SerializeField] private Transform[] secondTargetPosList;
        [SerializeField] private Transform endTargetPos;

        
        
        void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                var obj = Instantiate(zombieBoatPrefab);
                obj.transform.SetPositionAndRotation(startPos.position, Quaternion.identity);

                var queue = new Queue<Vector3>();
                queue.Enqueue(startPos.position);
                queue.Enqueue(firstTargetPosList[Random.Range(0, firstTargetPosList.Length)].position);
                queue.Enqueue(secondTargetPosList[Random.Range(0, secondTargetPosList.Length)].position);
                queue.Enqueue(endTargetPos.position);
                obj.GetComponent<ZombieBoat>().SetTargetPositions(queue);
            });
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

