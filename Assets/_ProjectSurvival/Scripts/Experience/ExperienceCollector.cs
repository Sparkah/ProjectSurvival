using _ProjectSurvival.Scripts.LevelingSystem;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Experience
{
    public class ExperienceCollector : MonoBehaviour
    {
        [SerializeField] private Behaviour _levelableObject;
        private ILevelable _levelable => _levelableObject as ILevelable;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ExperiencePoint experiencePoint))
            {
                if (experiencePoint.Collect(transform))
                {
                    _levelable.AddExperience(experiencePoint.ExperienceAmount);
                }
            }
        }
    }
}
