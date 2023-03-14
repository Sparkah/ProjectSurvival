using _ProjectSurvival.Scripts.UpgradeTree;
using UniRx;

namespace _ProjectSurvival.Infrastructure
{
    public class World
    {
        public ReactiveProperty<float> Gold { get; set; } = new ReactiveProperty<float>(0);

        public ReactiveDictionary<UpgradeTypes, int> UpgradesLevels = new ReactiveDictionary<UpgradeTypes, int>();

        public World()
        {
            InitUpgrades();
        }

        private void InitUpgrades()
        {
            UpgradesLevels.Add(UpgradeTypes.Vampirik,0);
            UpgradesLevels.Add(UpgradeTypes.MaxHealth,0);
            UpgradesLevels.Add(UpgradeTypes.MoveSpeed,0);
            UpgradesLevels.Add(UpgradeTypes.WP_01,0);
            UpgradesLevels.Add(UpgradeTypes.WP_03,0);
            UpgradesLevels.Add(UpgradeTypes.WP_05,0);
            UpgradesLevels.Add(UpgradeTypes.WP_07,0);
            UpgradesLevels.Add(UpgradeTypes.WP_09,0);
            UpgradesLevels.Add(UpgradeTypes.WP_10,0);
            UpgradesLevels.Add(UpgradeTypes.WP_12,0);
            UpgradesLevels.Add(UpgradeTypes.AllGunsCooldown,0);
            UpgradesLevels.Add(UpgradeTypes.AllGunsDamage,0);
        }
    }
}