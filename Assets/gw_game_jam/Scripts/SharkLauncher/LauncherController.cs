using System;
using gw_game_jam.Scripts.Score;
using UniRx;
using UnityEngine;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class LauncherController : MonoBehaviour
    {
        [SerializeField] private WhiteShark shark;
        [SerializeField] private GameObject launchPoint;

        private void Awake()
        {
            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Space))
                .ThrottleFirst(TimeSpan.FromSeconds(0.5)).Subscribe(_ => LaunchShark());
        }


        private void LaunchShark()
        {
            IPlayerBullet instantiateShark =
                Instantiate(shark, launchPoint.transform.position, launchPoint.transform.rotation);
            instantiateShark.AddForce(launchPoint.transform.forward * 10);
        }

        
    }
}