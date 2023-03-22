using UnityEngine;

namespace _ProjectSurvival.Scripts.LevelingSystem.Rewards
{
    public interface IReward
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Picture { get; }
        public RewardType GetRewardType();
        public string GetLevelUpDescription(int level); //TODO: Divide this logic
    }
}
