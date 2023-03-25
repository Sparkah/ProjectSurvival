using _ProjectSurvival.Scripts.UpgradeTree;
using UniRx;

namespace _ProjectSurvival.Infrastructure
{
    public class World
    {
        public ReactiveProperty<float> Gold { get; set; } = new ReactiveProperty<float>(0);
        public ReactiveProperty<bool> IsSoundOn { get; set; } = new ReactiveProperty<bool>(true);

        public ReactiveDictionary<UpgradeTypes, int> UpgradeLevels = new ReactiveDictionary<UpgradeTypes, int>();

        public ReactiveProperty<int> CurrentUpgradeID = new ReactiveProperty<int>();

        public World()
        {
            InitUpgrades();
        }

        private void InitUpgrades()
        {
            UpgradeLevels.Add(UpgradeTypes.Vampirik,0);
            UpgradeLevels.Add(UpgradeTypes.MaxHealth,0);
            UpgradeLevels.Add(UpgradeTypes.MoveSpeed,0);
            UpgradeLevels.Add(UpgradeTypes.WP_01,0);
            UpgradeLevels.Add(UpgradeTypes.WP_03,0);
            UpgradeLevels.Add(UpgradeTypes.WP_05,0);
            UpgradeLevels.Add(UpgradeTypes.WP_07,0);
            UpgradeLevels.Add(UpgradeTypes.WP_09,0);
            UpgradeLevels.Add(UpgradeTypes.WP_10,0);
            UpgradeLevels.Add(UpgradeTypes.WP_12,0);
            UpgradeLevels.Add(UpgradeTypes.AllGunsCooldown,0);
            UpgradeLevels.Add(UpgradeTypes.AllGunsDamage,0);
        }
    }
}