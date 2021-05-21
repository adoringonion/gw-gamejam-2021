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
        

        public void Fire()
        {
            IPlayerBullet instantiateShark =
                Instantiate(shark, launchPoint.transform.position, launchPoint.transform.rotation);
            instantiateShark.AddForce(launchPoint.transform.forward * 15);
        }
    }
}