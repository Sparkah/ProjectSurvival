using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ProjectSurvival.Scripts.Helpers
{
    public class RestartScene : MonoBehaviour
    {
        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
