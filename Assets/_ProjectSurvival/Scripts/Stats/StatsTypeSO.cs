using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.UpgradeTree;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Stats
{
    [CreateAssetMenu(fileName = "New stat type", menuName = "Survivors prototype/Stat type", order = 1)]
    public class StatsTypeSO : ScriptableObject, IReward
    {
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _picture;
        [SerializeField] private int[] _statAmountPercentIncrease;
        [SerializeField] private UpgradeTypes _upgradeType;

        public string Title => _title;
        public string Description => _description;
        public Sprite Picture => _picture;
        public int[] StatsIncrease => _statAmountPercentIncrease;
        public UpgradeTypes UpgradeType => _upgradeType;

        public RewardType GetRewardType()
        {
            return RewardType.Stat;
        }
    }
}