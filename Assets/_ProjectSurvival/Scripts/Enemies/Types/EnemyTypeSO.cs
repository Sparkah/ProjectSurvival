using _ProjectSurvival.Scripts.Enemies.Abilities;
using _ProjectSurvival.Scripts.LevelingSystem;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Types
{
    [CreateAssetMenu(fileName = "New enemy type", menuName = "Survivors prototype/Enemy type", order = 1)]
    public class EnemyTypeSO : ScriptableObject
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Color _typeColor;
        [SerializeField] private LevelingSchemeSO _levelingSchemeSO;
        [SerializeField] private EnemyLevelData[] _enemyLevels;
        //[SerializeField] private EnemyAbilities _enemyAbilities;

        public Enemy EnemyPrefab => _enemyPrefab;
        public int MaximumEvolutionLevel => _enemyLevels.Length;
        public EnemyLevelData[] EnemyLevels => _enemyLevels;
        public Color TypeColor => _typeColor;
        public LevelingSchemeSO LevelingSchemeSO => _levelingSchemeSO;
        
       // public EnemyAbilities Ability => _enemyAbilities;

        public EnemyLevelData GetEnemyLevelData(int level)
        {
            return _enemyLevels[ValidateIndex(level-1)];
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
