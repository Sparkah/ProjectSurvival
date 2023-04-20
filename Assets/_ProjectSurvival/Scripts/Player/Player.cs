using _ProjectSurvival.Scripts.DamageSystem;
using _ProjectSurvival.Scripts.LevelingSystem;
using _ProjectSurvival.Scripts.Stats;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private LevelableObject _levelableObject;
        [SerializeField] private DamagableObject _damagableObject;
        [SerializeField] private float _playerBaseHealth = 30;
        private float _initialHealth;
        private ActiveStats _activeStats;

        [Inject]
        private void Construct(ActiveStats activeStats)
        {
            _initialHealth = _playerBaseHealth;
            _activeStats = activeStats;
            _activeStats.OnMaxHealthStatChanged += IncreasePlayerHealth;
        }
        private void Start()
        {
            _levelableObject.Init();
            _playerAttack.StartFire();
            _damagableObject.SetupHealth(_playerBaseHealth);
        }

        private void IncreasePlayerHealth(float percentage)
        {
            _playerBaseHealth = _initialHealth + (_initialHealth*percentage)/100;
            _damagableObject.IncreaseMaxHealth(_playerBaseHealth);
        }

        private void OnDestroy()
        {
            _activeStats.OnMaxHealthStatChanged -= IncreasePlayerHealth;
        }
    }
}