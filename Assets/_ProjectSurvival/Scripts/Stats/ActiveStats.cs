using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Stats
{
    public class ActiveStats : MonoBehaviour, IRewardGiver
    {
        public void GiveReward(IReward reward)
        {
            if (reward.GetRewardType() == RewardType.Stat)
            {
                Debug.Log($"{RewardType.Stat} is a reward type that is not implemented");
            }
        }
    }
}