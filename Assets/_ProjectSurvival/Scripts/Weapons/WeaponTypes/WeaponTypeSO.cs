using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.UpgradeTree;
using _ProjectSurvival.Scripts.Weapons.Projectiles;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.WeaponTypes
{
    [CreateAssetMenu(fileName = "New weapon type", menuName = "Survivors prototype/Weapon type", order = 1)]
    public class WeaponTypeSO : ScriptableObject, IReward
    {
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private string _descriptionForUPGR;
        [SerializeField] private Sprite _picture;
        [SerializeField] private AudioSounds _shootSound;
        [SerializeField] private WeaponProjectile _projectilePrefab;
        [SerializeField] private WeaponLevel[] _weaponLevels;
        [SerializeField] private UpgradeTypes _upgradeType;

        public string Title => _title;
        public string Description => _description;
        public string Description1 => _descriptionForUPGR;
        public Sprite Picture => _picture;
        public WeaponProjectile ProjectilePrefab => _projectilePrefab;
        public int MaximumLevel => _weaponLevels.Length;
        public AudioSounds ShootSound => _shootSound;
        public UpgradeTypes UpgradeType => _upgradeType;

        public RewardType GetRewardType()
        {
            return RewardType.Weapon;
        }

        public float GetAppearFrequency(int level)
        {
            return _weaponLevels[level - 1].AppearFrequency;
        }

        public int GetAppearAmount(int level)
        {
            return _weaponLevels[level - 1].AppearAmount;
        }

        public ProjectileSettings GetProjectileSettings(int level)
        {
            return _weaponLevels[level - 1].ProjectileSettingSO;
        }

        public string GetLevelUpDescription(int level)
        {
            return _weaponLevels[level - 1].Description;
        }

        [System.Serializable]
        private struct WeaponLevel
        {
            [SerializeField] private float _appearFrequency;
            [SerializeField] private int _appearAmount;
            [SerializeField] private ProjectileSettings _projectileSetting;
            [SerializeField] private string _description;
            [SerializeField] private string _descriptionForUPGR;

            public float AppearFrequency => _appearFrequency;
            public int AppearAmount => _appearAmount;
            public ProjectileSettings ProjectileSettingSO => _projectileSetting;
            public string Description => _description;
            public string DescriptionForUPGR => _descriptionForUPGR;
        }
    }
}
