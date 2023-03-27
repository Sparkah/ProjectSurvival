using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.LevelingSystem
{
    public class LevelableObject : MonoBehaviour, ILevelable
    {
        [SerializeField] private LevelingSchemeSO _levelingSchemeSO;
        private int _level;
        private float _currentExperience;
        private float _requiredExperience;

        public float CurrentExperience => _currentExperience;
        public float RequiredExperience => _requiredExperience;
        public int Level => _level;
        public bool IsMaximumLevel => (!HasNextLevel());// && (_currentExperience == _requiredExperience);

        public event UnityAction OnExperienceChanged;
        public event UnityAction OnLevelUp;

        public void Init()
        {
            AddExperience(0);
        }

        public void AddExperience(float amount)
        {
            if (_currentExperience >= _requiredExperience)
                LevelUp();
            OnExperienceChanged?.Invoke();
        }

        private bool HasNextLevel()
        {
            return _levelingSchemeSO.HasNextLevel(_level);
        }

        private void LevelUp()
        {
            _currentExperience -= _requiredExperience;
            _level++;
            _requiredExperience = _levelingSchemeSO.GetRequiredExperienceForLevel(_level + 1);
            OnLevelUp?.Invoke();
        }
    }
}
