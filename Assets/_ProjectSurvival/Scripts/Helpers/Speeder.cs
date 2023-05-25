using Sirenix.OdinInspector;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Helpers
{
    public class Speeder : MonoBehaviour
    {
        [Button]
        public void SpeedUp(int speed)
        {
            Time.timeScale *= speed;
        }
    
        [Button]
        public void SlowDown()
        {
            Time.timeScale *= 1;
        }
    }
}
