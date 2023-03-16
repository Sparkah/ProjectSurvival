using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "New enemy type", menuName = "Survivors prototype/Enemy type", order = 1)]
    public class EnemyTypeSO : ScriptableObject
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Color _typeColor;
        [SerializeField] private EnemyLevelData[] _enemyLevels;

        public Enemy EnemyPrefab => _enemyPrefab;
        public int MaximumEvolutionLevel => _enemyLevels.Length;
        public Color TypeColor => _typeColor;

        public EnemyLevelData GetEnemyLevelData(int level)
        {
            return _enemyLevels[ValidateIndex(level)];
        }

        public int GetRequirementToEvolution(int currentLevel)
        {
            return _enemyLevels[ValidateIndex(currentLevel)].AmountToEvolve;
        }

        private int ValidateIndex(int index)
        {
            int validatedIndex = index;
            if (index < 0)
                validatedIndex = 0;
            if (index >= _enemyLevels.Length)
                validatedIndex = _enemyLevels.Length - 1;
            return validatedIndex;
        }
    }
}
