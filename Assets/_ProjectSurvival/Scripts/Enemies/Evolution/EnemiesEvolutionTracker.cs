using _ProjectSurvival.Scripts.LevelingSystem;
using System.Collections.Generic;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Evolution
{
    public class EnemiesEvolutionTracker : MonoBehaviour
    {
        private Dictionary<EnemyTypeSO, ILevelable> _evolutionExperience = new Dictionary<EnemyTypeSO, ILevelable>();

        public void IncreaseEvolutionExperience(EnemyTypeSO enemyType, float experience)
        {
            if (!_evolutionExperience.ContainsKey(enemyType))
                _evolutionExperience.Add(enemyType, new EvolutionLevel(enemyType.LevelingSchemeSO));
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
    }
}
