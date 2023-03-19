using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies
{
    [System.Serializable]
    public class EnemyLevelData
    {
        [SerializeField] private Sprite _appearanceSpriteFront;
        [SerializeField] private Sprite _appearanceSpriteBack;
        [SerializeField] private float _baseHealth;
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseExperience;
        [SerializeField] private float _baseGold;

        public float BaseHealth => _baseHealth;
        public float BaseSpeed => _baseSpeed;
        public float BaseDamage => _baseDamage;
        public float BaseExperience => _baseExperience;
        public float BaseGold => _baseGold;
        public Sprite AppearanceSpriteFront => _appearanceSpriteFront;
        public Sprite AppearanceSpriteBack => _appearanceSpriteBack;
    }
}
