using UnityEngine;

namespace _ProjectSurvival.Scripts.Experience
{
    [CreateAssetMenu(fileName = "New experience point settings", menuName = "Survivors prototype/Experience point settings", order = 1)]
    public class ExperiencePointSettingsSO : ScriptableObject
    {
        [SerializeField] private float _gatheringSpeed = 10;
        [SerializeField] private float _rotatingSpeed = 0.1f;
        [SerializeField] private float _approximatelyDistance;

        public float GatheringSpeed => _gatheringSpeed;
        public float RotatingSpeed => _rotatingSpeed;
        public float ApproximatelyDistance => _approximatelyDistance; 
    }
}
