using UnityEngine;

namespace gw_game_jam.Scripts
{
    public class LauncherController : MonoBehaviour
    {
        [SerializeField] private GameObject shark;
        [SerializeField] private GameObject launchPoint;
        private const float LAUNCH_SPEED = 10000f;
        
        
        private void Start()
        {
        
        }

        // Update is called once per frame
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
            GameObject instantiateShark = Instantiate(shark);
            instantiateShark.transform.position = launchPoint.transform.position;
            instantiateShark.GetComponent<Rigidbody>().AddForce(launchPoint.transform.forward * LAUNCH_SPEED);
        }
    }
}
