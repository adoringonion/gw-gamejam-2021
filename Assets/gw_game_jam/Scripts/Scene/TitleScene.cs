using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

namespace gw_game_jam.Scene
{
    /// <summary>
    /// Titleシーン.
    /// </summary>
    public class TitleScene : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            /*
            this.UpdateAsObservable().Where(_ => OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                .ThrottleFirst(TimeSpan.FromSeconds(0.5)).Subscribe(_ => launcherController.Fire()).AddTo(this);
            */
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                .ThrottleFirst(TimeSpan.FromSeconds(0.5)).Subscribe(_ => SceneManager.LoadScene("main")).AddTo(this);
        }
        

        private void Update()
        {
        }
    }
}
