using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.LevelingSystem
{
    public interface ILevelable
    {
        public float CurrentExperience { get; }
        public float RequiredExperience { get; }
        public int Level { get; }

        public event UnityAction OnExperienceChanged;
        public event UnityAction OnLevelUp;

        public void AddExperience(float amount);
    }
}
