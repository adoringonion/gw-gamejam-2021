using System;
using UnityEngine;
using UniRx;


using gw_game_jam.Scripts.SharkLauncher;
using UniRx.Triggers;

namespace gw_game_jam.Player
{
    /// <summary>
    /// プレイヤー.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private LauncherController launcherController;

        
        private void Start()
        {
            this.UpdateAsObservable().Where(_ => OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                .ThrottleFirst(TimeSpan.FromSeconds(0.5)).Subscribe(_ => launcherController.Fire()).AddTo(this);
        }

    }
}

