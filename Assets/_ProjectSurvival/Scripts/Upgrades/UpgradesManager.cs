using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Upgrades
{
    public class UpgradesManager : MonoBehaviour
    {
        [SerializeField] private StatsTypeSO[] _statUpgrades;
        [SerializeField] private WeaponTypeSO[] _weaponUpgrades;
        
        private World _world;
        
        [Inject]
        public void Construct(World world)
        {
            _world = world;
            UnpackPurchasedUpgrades();
        }

        private void UnpackPurchasedUpgrades()
        {
            Debug.Log("Unpacking upgrades");

            foreach (var upgradeLevel in _world.UpgradeLevels)
            {
                //if(upgradeLevel.Key = )
                Debug.Log("upgrade unpacked");
            }
        }
    }
}
