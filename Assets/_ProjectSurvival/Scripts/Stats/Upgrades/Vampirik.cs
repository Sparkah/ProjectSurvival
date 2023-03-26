using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.DamageSystem;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Stats.Upgrades
{
    [RequireComponent(typeof(DamagableObject))]
    public class Vampirik : MonoBehaviour
    { 
        private DamagableObject _damagableObjectSelf;
        private DamagableObject _damagableObjectPlayer;
        private ActiveStats _activeStats;
        private World _world;
        private float _healPercentage = 0;
        private DamageDealer _playerDamageDealer;

        [Inject]
        public void Construct(ActiveStats activeStats, World world, Player.Player player)
        {
            _damagableObjectSelf = GetComponent<DamagableObject>();
            _damagableObjectPlayer = player.GetComponent<DamagableObject>();
            _activeStats = activeStats;
            IncreaseVampirism(_activeStats.GetVampirikAmount());
        }
        
        private void Awake()
        {
            _damagableObjectSelf.OnDamageAmount += RestorePlayer;
        }

        private void IncreaseVampirism(float percentage)
        {
            _healPercentage += percentage / 100;
        }

        private void RestorePlayer(float damage)
        {
            var heal = damage * _healPercentage;
            _damagableObjectPlayer.RestoreHP(heal);
        }

        private void OnDestroy()
        {
            _damagableObjectSelf.OnDamageAmount -= RestorePlayer;
        }
    }
}