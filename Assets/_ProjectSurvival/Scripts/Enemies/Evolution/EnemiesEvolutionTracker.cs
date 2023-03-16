using System.Collections.Generic;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Evolution
{
    public class EnemiesEvolutionTracker : MonoBehaviour
    {
        private Dictionary<EnemyTypeSO, int> _defeatedEnemiesCounter = new Dictionary<EnemyTypeSO, int>();

        public event System.Action<EnemyTypeSO> OnEvolve;

        public void IncreaseDefeatedCount(EnemyTypeSO enemyType)
        {
            if (!_defeatedEnemiesCounter.ContainsKey(enemyType))
                _defeatedEnemiesCounter.Add(enemyType, 1);
            else
                _defeatedEnemiesCounter[enemyType]++;

            if (CanEvolve(enemyType))
                Evolve(enemyType);
        }

        public int GetEnemyEvolutionLevel(EnemyTypeSO enemyType)
        {
            int evolutionLevel = 0;
            if (!_defeatedEnemiesCounter.ContainsKey(enemyType))
                return evolutionLevel;

            int defeatedAmount = _defeatedEnemiesCounter[enemyType];
            for (int i = 0; i < enemyType.MaximumEvolutionLevel; i++)
            {
                if (defeatedAmount >= enemyType.GetRequirementToEvolution(i))
                    evolutionLevel = i;
                else
                    break;
            }
            return evolutionLevel;
        }

        private bool CanEvolve(EnemyTypeSO enemyType)
        {
            if (!_defeatedEnemiesCounter.ContainsKey(enemyType))
                return false;

            int defeatedAmount = _defeatedEnemiesCounter[enemyType];
            for (int i = 0; i < enemyType.MaximumEvolutionLevel; i++)
            {
                if (defeatedAmount == enemyType.GetRequirementToEvolution(i))
                    return true;
            }
            return false;
        }

        private void Evolve(EnemyTypeSO enemyType)
        {
            OnEvolve?.Invoke(enemyType);
        }
    }
}
