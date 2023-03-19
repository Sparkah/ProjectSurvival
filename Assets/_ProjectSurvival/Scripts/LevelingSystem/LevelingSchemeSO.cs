using UnityEngine;

namespace _ProjectSurvival.Scripts.LevelingSystem
{
    [CreateAssetMenu(fileName = "New leveling scheme", menuName = "Survivors prototype/Leveling scheme", order = 1)]
    public class LevelingSchemeSO : ScriptableObject
    {
        [SerializeField] private float[] _requiredExperience;

        public float GetRequiredExperienceForLevel(int level)
        {
            int levelIndex = level - 1;

            if (levelIndex >= _requiredExperience.Length)
                return _requiredExperience[_requiredExperience.Length - 1];
            else if (levelIndex >= 0)
                return _requiredExperience[levelIndex];

            return 0;
        }

        public bool HasNextLevel(int level)
        {
            int levelIndex = level - 1;
            return levelIndex + 1 < _requiredExperience.Length;
        }
    }
}
