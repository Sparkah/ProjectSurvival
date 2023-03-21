using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.UpgradeTree;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Stats
{
    public class ActiveStats : MonoBehaviour, IRewardGiver
    {
        public event UnityAction<float> OnVampirikStatChanged;
        public event UnityAction<float> OnBaseDamageStatChanged;
        public event UnityAction<float> OnBaseCooldownStatChanged;
        public event UnityAction<float> OnWalkSpeedStatChanged;
        public event UnityAction<float> OnMaxHealthStatChanged;

        private int _vampirikUpgrades = 0;
        private int _baseDamageUpgrades = 0;
        private int _baseCooldownUpgrades = 0;
        private int _walkSpeedUpgrades = 0;
        private int _maxHealthUpgrades = 0;
        
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
                    OnBaseCooldownStatChanged?.Invoke(statReward.StatsIncrease[_baseDamageUpgrades]);
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
    }
}