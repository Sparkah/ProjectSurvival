using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.UpgradeTree;
using _ProjectSurvival.Scripts.Weapons.ActiveWeapons;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Upgrades
{
    public class UpgradesManager : MonoBehaviour, IRewardGiver
    {
        [SerializeField] private AvailableWeaponsSO _availableWeaponsSo;
        [SerializeField] private AvailableStatsSO _availableStatsSo;
        
        private World _world;
        private ActiveWeapons _activeWeapons;
        private ActiveStats _activeStats;
        
        [Inject]
        public void Construct(World world, ActiveWeapons activeWeapons, ActiveStats activeStats)
        {
            _world = world;
            _activeWeapons = activeWeapons;
            _activeStats = activeStats;
            UnpackPurchasedUpgrades();
        }

        private void UnpackPurchasedUpgrades()
        {
            foreach (var upgradeLevel in _world.UpgradeLevels)
            {
                switch (upgradeLevel.Key)
                {
                    case UpgradeTypes.WP_01:
                        UpgradeWeapon(UpgradeTypes.WP_01, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.WP_03:
                        UpgradeWeapon(UpgradeTypes.WP_03, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.WP_05:
                        UpgradeWeapon(UpgradeTypes.WP_05, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.WP_07:
                        UpgradeWeapon(UpgradeTypes.WP_07, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.WP_09:
                        UpgradeWeapon(UpgradeTypes.WP_09, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.WP_10:
                        UpgradeWeapon(UpgradeTypes.WP_10, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.WP_12:
                        UpgradeWeapon(UpgradeTypes.WP_12, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.Vampirik:
                        UpgradeStats(UpgradeTypes.Vampirik, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.MaxHealth:
                        UpgradeStats(UpgradeTypes.MaxHealth, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.MoveSpeed:
                        UpgradeStats(UpgradeTypes.MoveSpeed, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.AllGunsCooldown:
                        UpgradeStats(UpgradeTypes.AllGunsCooldown, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.AllGunsDamage:
                        UpgradeStats(UpgradeTypes.AllGunsDamage, upgradeLevel.Value);
                        break;
                    case UpgradeTypes.None:
                    default: 
                        Debug.Log("Upgrade not implemented");
                        break;
                }
            }
        }

        public int GetUpgrade(UpgradeTypes upgradeType)
        {
            return _world.UpgradeLevels[upgradeType];
        }

        public void GiveReward(IReward reward)
        {
            _activeStats.GiveReward(reward);
        }

        private void UpgradeStats(UpgradeTypes upgradeType, int value)
        {
            var stats = _availableStatsSo.SelectAvailableStats();
            foreach (var stat in stats)
            {
                if (stat.UpgradeType == upgradeType)
                {
                    for (int i = 0; i < value; i++)
                    {
                        _activeStats.AddStat(stat);
                    }
                }
            }
        }

        private void UpgradeWeapon(UpgradeTypes upgradeType, int value)
        {
            var weapons = _availableWeaponsSo.SelectAvailableWeapons(_activeWeapons);
            foreach (var weapon in weapons)
            {
                if (weapon.UpgradeType == upgradeType)
                {
                    for (int i = 0; i < value; i++)
                    {
                        _activeWeapons.AddWeaponUpgradesToQue(weapon);
                    }
                }
            }
        }
    }
}
