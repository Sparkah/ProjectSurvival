using UnityEngine;

namespace _ProjectSurvival.Scripts.Experience
{
    [CreateAssetMenu(fileName = "New experience visual table", menuName = "Survivors prototype/Experience visual table", order = 1)]
    public class ExperienceVisualTableSO : ScriptableObject
    {
        [Header("Place in order (0 - N)")]
        [SerializeField] private ExperiencePointVisual[] _experiencePointVisuals;

        public ExperiencePointVisual GetVisualForExperienceAmount(float experienceAmount)
        {
            for (int i = 0; i < _experiencePointVisuals.Length; i++)
            {
                if (experienceAmount <= _experiencePointVisuals[i].ExperienceAmount)
                    return _experiencePointVisuals[i];
            }
            return _experiencePointVisuals[_experiencePointVisuals.Length - 1];
        }
    }
}
