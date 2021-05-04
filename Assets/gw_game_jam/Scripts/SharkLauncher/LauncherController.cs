using UnityEngine;

namespace gw_game_jam.Scripts.SharkLauncher
{
    public class LauncherController : MonoBehaviour
    {
        [SerializeField] private GameObject shark;
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
            GameObject instantiateShark = Instantiate(shark, launchPoint.transform.position, launchPoint.transform.rotation);
            instantiateShark.GetComponent<Rigidbody>().AddForce(launchPoint.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
