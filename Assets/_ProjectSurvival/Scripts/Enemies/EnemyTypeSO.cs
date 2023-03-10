using _ProjectSurvival.Scripts.Enemies;
using UnityEngine;


[CreateAssetMenu(fileName = "New enemy type", menuName = "Survivors prototype/Enemy type", order = 1)]
public class EnemyTypeSO : ScriptableObject
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Sprite _appearanceSpriteFront;
    [SerializeField] private Sprite _appearanceSpriteBack;
    [SerializeField] private float _baseHealth;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _baseExperience;
    [SerializeField] private float _baseGold;

    public Enemy EnemyPrefab => _enemyPrefab;
    public float BaseHealth => _baseHealth;
    public float BaseSpeed => _baseSpeed;
    public float BaseDamage => _baseDamage;
    public float BaseExperience => _baseExperience;
    public float BaseGold => _baseGold;
    public Sprite AppearanceSpriteFront => _appearanceSpriteFront;
    public Sprite AppearanceSpriteBack => _appearanceSpriteBack;
}
