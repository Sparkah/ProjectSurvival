using System.Collections.Generic;
using _ProjectSurvival.Scripts.Enemies;
using UnityEngine;

namespace _ProjectSurvival.Scripts.AI
{
    [CreateAssetMenu(fileName = "New MonsterTypeHolder", menuName = "Survivors prototype/Monster types holder", order = 1)]
    public class MonsterTypesSO : ScriptableObject
    {
        public List <Sprite> Sprites;

        [SerializeField] private EnemyTypeSO[] _enemyTypeSos;

        public void ChangeSprites()
        {
            _enemyTypeSos[0].EnemyLevels[0].AppearanceSpriteFront = Sprites[0];
            _enemyTypeSos[0].EnemyLevels[0].AppearanceSpriteBack = Sprites[0];
        }

        public void ClearSprites()
        {
            Sprites.Clear();
        }
    }
}