using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon type", menuName = "Survivors prototype/Weapon type", order = 1)]
public class WeaponTypeSO : ScriptableObject, IReward
{
    [SerializeField] private string _title;
    [SerializeField] private AudioSounds _shootSound;
    [SerializeField] private WeaponProjectile _projectilePrefab;
    [SerializeField] private WeaponLevel[] _weaponLevels;

    public string Title => _title;
    public RewardType GetRewardType()
    {
        return RewardType.Weapon;
    }

    public WeaponProjectile ProjectilePrefab => _projectilePrefab;
    public int MaximumLevel => _weaponLevels.Length;
    public AudioSounds ShootSound => _shootSound;

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

    [System.Serializable]
    private struct WeaponLevel
    {
        [SerializeField] private float _appearFrequency;
        [SerializeField] private int _appearAmount;
        [SerializeField] private ProjectileSettings _projectileSetting;

        public float AppearFrequency => _appearFrequency;
        public int AppearAmount => _appearAmount;
        public ProjectileSettings ProjectileSettingSO => _projectileSetting;
    }
}
