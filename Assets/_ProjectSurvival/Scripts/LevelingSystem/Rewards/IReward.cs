namespace _ProjectSurvival.Scripts.LevelingSystem.Rewards
{
    public interface IReward
    {
        public string Title { get; }
        public RewardType GetRewardType();
    }
}
