using UniRx;
using UnityEngine;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class LauncherController : MonoBehaviour
    {
        [SerializeField] private WhiteShark shark;
        [SerializeField] private GameObject launchPoint;
        
        private void Update()
        {
            TestInput();
        }

        private void TestInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchShark();
            }
        }

        private void LaunchShark()
        {
            WhiteShark instantiateShark = Instantiate(shark, launchPoint.transform.position, launchPoint.transform.rotation);
            instantiateShark.AddForce(launchPoint.transform.forward * 10);
            instantiateShark.OnHitAsync.Take(1).Subscribe(_ => Debug.Log("ゾンビ倒した"));
        }
    }
}
