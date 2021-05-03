using UnityEngine;

namespace gw_game_jam.Scripts
{
    public class LauncherController : MonoBehaviour
    {
        [SerializeField] private GameObject shark;
        // Start is called before the first frame update
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
                //TODO: 発射するスクリプト
            }
        }
    }
}
