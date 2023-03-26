using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.UpgradeTree;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Stats
{
    public class ActiveStats : MonoBehaviour, IRewardGiver
    {
        public event UnityAction<float> OnVampirikStatChanged; //Get/SetVampirikAmount methods used for this logic (runtime enemies vampirik class)
        public event UnityAction<float> OnBaseDamageStatChanged;
        public event UnityAction<float> OnBaseCooldownStatChanged;
        public event UnityAction<float> OnWalkSpeedStatChanged;
        public event UnityAction<float> OnMaxHealthStatChanged;

        private int _vampirikUpgrades = 0;
        private int _baseDamageUpgrades = 0;
        private int _baseCooldownUpgrades = 0;
        private int _walkSpeedUpgrades = 0;
        private int _maxHealthUpgrades = 0;
        private float _vampirism;
        
        public void GiveReward(IReward reward)
        {
            if (reward.GetRewardType() == RewardType.Stat)
            {
                var statReward = (StatsTypeSO) reward;
                Add(statReward);
            }
        }

        public void AddStat(StatsTypeSO stat)
        {
            Add(stat);
        }

        private void Add(StatsTypeSO statReward)
        {
            switch (statReward.UpgradeType)
            {
                case UpgradeTypes.Vampirik:
                    OnVampirikStatChanged?.Invoke(statReward.StatsIncrease[_vampirikUpgrades]);
                    SetVampirikAmount(statReward.StatsIncrease[_vampirikUpgrades]);
                    _vampirikUpgrades += 1;
                    break;
                case UpgradeTypes.MaxHealth:
                    OnMaxHealthStatChanged?.Invoke(statReward.StatsIncrease[_maxHealthUpgrades]);
                    _maxHealthUpgrades += 1;
                    break;
                case UpgradeTypes.MoveSpeed:
                    OnWalkSpeedStatChanged?.Invoke(statReward.StatsIncrease[_walkSpeedUpgrades]);
                    _walkSpeedUpgrades += 1;
                    break;
                case UpgradeTypes.AllGunsCooldown:
                    OnBaseCooldownStatChanged?.Invoke(statReward.StatsIncrease[_baseCooldownUpgrades]);
                    _baseCooldownUpgrades += 1;
                    break;
                case UpgradeTypes.AllGunsDamage:
                    OnBaseDamageStatChanged?.Invoke(statReward.StatsIncrease[_baseDamageUpgrades]);
                    _baseDamageUpgrades += 1;
                    break;
                default:
                    Debug.Log("Not implemented");
                    break;
            }
        }

        private void SetVampirikAmount(float amount)
        {
            _vampirism = amount;
        }

        public float GetVampirikAmount()
        {
            return _vampirism;
        }

        public int GetRewardLevel(IReward reward)
        {
            if (reward.GetRewardType() != RewardType.Stat)
                return -1;

            StatsTypeSO statReward = (StatsTypeSO)reward;

            switch (statReward.UpgradeType)
            {
                case UpgradeTypes.Vampirik:
                    return _vampirikUpgrades;
                case UpgradeTypes.MaxHealth:
                    return _maxHealthUpgrades;
                case UpgradeTypes.MoveSpeed:
                    return _walkSpeedUpgrades;
                case UpgradeTypes.AllGunsCooldown:
                    return _baseCooldownUpgrades;
                case UpgradeTypes.AllGunsDamage:
                    return _baseDamageUpgrades;
                default:
                    Debug.Log("Not implemented");
                    return -1;
            }
        }

        public bool HasMaximumLevel(StatsTypeSO statType)
        {
            int currentLevel = 0;
            switch (statType.UpgradeType)
            {
                case UpgradeTypes.Vampirik:
                    currentLevel = _vampirikUpgrades;
                    break;
                case UpgradeTypes.MaxHealth:
                    currentLevel = _maxHealthUpgrades;
                    break;
                case UpgradeTypes.MoveSpeed:
                    currentLevel = _walkSpeedUpgrades;
                    break;
                case UpgradeTypes.AllGunsCooldown:
                    currentLevel = _baseCooldownUpgrades;
                    break;
                case UpgradeTypes.AllGunsDamage:
                    currentLevel = _baseDamageUpgrades;
                    break;
                default:
                    Debug.Log("Not implemented");
                    return false;
            }
            return !CanLevelUp(currentLevel, statType);
        }

        private bool CanLevelUp(int currentLevel, StatsTypeSO statType)
        {
            return currentLevel + 1 <= statType.StatsIncrease.Length; 
        }
    }
}