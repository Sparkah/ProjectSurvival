using UnityEngine;


[CreateAssetMenu(fileName = "New enemy type", menuName = "Survivors prototype/Enemy type", order = 1)]
public class EnemyTypeSO : ScriptableObject
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Sprite _appearanceSprite;
    [SerializeField] private float _baseHealth;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _baseExperience;

    public Enemy EnemyPrefab => _enemyPrefab;
    public float BaseHealth => _baseHealth;
    public float BaseSpeed => _baseSpeed;
    public float BaseDamage => _baseDamage;
    public float BaseExperience => _baseExperience;
    public Sprite AppearanceSprite => _appearanceSprite;
}
