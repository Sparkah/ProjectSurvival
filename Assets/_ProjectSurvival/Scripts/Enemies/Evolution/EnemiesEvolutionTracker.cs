using _ProjectSurvival.Scripts.LevelingSystem;
using System.Collections.Generic;
using _ProjectSurvival.Scripts.Enemies.Types;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Evolution
{
    public class EnemiesEvolutionTracker : MonoBehaviour
    {
        private Dictionary<EnemyTypeSO, ILevelable> _evolutionExperience = new Dictionary<EnemyTypeSO, ILevelable>();

        public event System.Action<EnemyTypeSO> OnEnemyAdded;

        public void IncreaseEvolutionExperience(EnemyTypeSO enemyType, float experience)
        {
            if (!_evolutionExperience.ContainsKey(enemyType))
                StartTrackingExperience(enemyType);
            else
                _evolutionExperience[enemyType].AddExperience(experience);
        }

        public int GetEnemyEvolutionLevel(EnemyTypeSO enemyType)
        {
            if (!_evolutionExperience.ContainsKey(enemyType))
                return 1;
            else
                return _evolutionExperience[enemyType].Level;
        }

        public ILevelable GetEnemyLeveling(EnemyTypeSO enemyType)
        {
            if (!_evolutionExperience.ContainsKey(enemyType))
                return null;
            else
                return _evolutionExperience[enemyType];
        }

        public List<EnemyTypeSO> GetEvolvingEnemies()
        {
            return new List<EnemyTypeSO>(_evolutionExperience.Keys);
        }

        private void StartTrackingExperience(EnemyTypeSO enemyType)
        {
            _evolutionExperience.Add(enemyType, new EvolutionLevel(enemyType.LevelingSchemeSO));
            OnEnemyAdded?.Invoke(enemyType);
        }
    }
}
