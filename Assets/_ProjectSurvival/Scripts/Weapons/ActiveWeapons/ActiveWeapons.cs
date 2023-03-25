using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.Projectiles;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.ActiveWeapons
{
    public class ActiveWeapons : MonoBehaviour, IRewardGiver
    {
        [Inject] DiContainer _diContainer;
        [SerializeField] private ProjectilesPool _projectilePoolPrefab;
        private List<ActiveWeapon> _activeWeapons = new List<ActiveWeapon>();
        private ReactiveDictionary<WeaponTypeSO, int> _upgradesQue = new ReactiveDictionary<WeaponTypeSO, int>();
        private RewardType _rewardType = RewardType.Weapon;
        private ActiveStats _activeStats;
        private float _cooldownDecrease;

        public event UnityAction<ActiveWeapon> OnFire;

        [Inject]
        private void Construct(ActiveStats activeStats) 
        {
            _activeStats = activeStats;
            _activeStats.OnBaseCooldownStatChanged += SetCooldowDecreasePercent;
        }


        private void OnDestroy()
        {
            for (int i = 0; i < _activeWeapons.Count; i++)
            {
                _activeWeapons[i].OnFire -= RequestFire;
                _activeWeapons[i].CancelFiring();
            }
            _activeStats.OnBaseCooldownStatChanged -= SetCooldowDecreasePercent;
        }

        public void GiveReward(IReward reward)
        {
            if (reward.GetRewardType() == _rewardType)
                AddWeapon((WeaponTypeSO)reward);
        }

        public void AddWeaponUpgradesToQue(WeaponTypeSO weaponType)
        {
            if (_upgradesQue.ContainsKey(weaponType))
            {
                _upgradesQue[weaponType] += 1;
            }
            else
            {
                _upgradesQue.Add(weaponType,1);
            }
        }

        public void AddWeapon(WeaponTypeSO weaponType)
        {
            ActiveWeapon selectedWeapon = FindWeapon(weaponType);
            if (selectedWeapon == null)
            {
                ProjectilesPool pool = _diContainer.InstantiatePrefabForComponent<ProjectilesPool>(_projectilePoolPrefab, transform);
                pool.InitPool(weaponType.ProjectilePrefab);
                selectedWeapon = new ActiveWeapon(weaponType, pool);
                selectedWeapon.ChangeCooldownDecrease(_cooldownDecrease);
                selectedWeapon.OnFire += RequestFire;
                selectedWeapon.StartFiring();
                _activeWeapons.Add(selectedWeapon);
                AddExtraLevelUpFromSkillUpgrades(selectedWeapon, weaponType);
            }
            else
            {
                selectedWeapon.LevelUp();
            }
        }

        public bool HasMaximumLevel(WeaponTypeSO weaponType)
        {
            ActiveWeapon selectedWeapon = FindWeapon(weaponType);
            if (selectedWeapon == null)
            {
                _upgradesQue.TryGetValue(weaponType, out int quedAmount);
                if (quedAmount <= 0)
                    return false;
                else
                    return quedAmount >= weaponType.MaximumLevel;
            }
            return selectedWeapon.HasMaximumLevel();
        }

        public int GetRewardLevel(IReward reward)
        {
            if (reward.GetRewardType() != _rewardType)
                return -1;

            ActiveWeapon selectedWeapon = FindWeapon((WeaponTypeSO)reward);
            if (selectedWeapon == null)
            {
                _upgradesQue.TryGetValue((WeaponTypeSO)reward, out int quedAmount);
                if (quedAmount <= 0)
                    return 0;
                else
                    return quedAmount;
            }
            return selectedWeapon.Level;
        }

        public bool HasWeapon(WeaponTypeSO weaponType)
        {
            return FindWeapon(weaponType) != null;
        }

        private void AddExtraLevelUpFromSkillUpgrades(ActiveWeapon selectedWeapon, WeaponTypeSO weaponType)
        {
            _upgradesQue.TryGetValue(weaponType, out int quedAmount);
            if (quedAmount <= 0) return;
            for (var i = 0; i < quedAmount; i++)
            {
                selectedWeapon.LevelUp();
            }
        }

        private ActiveWeapon FindWeapon(WeaponTypeSO weaponType)
        {
            return _activeWeapons.Find(x => x.WeaponType == weaponType);
        }

        private void RequestFire(ActiveWeapon activeWeapon)
        {
            OnFire?.Invoke(activeWeapon);
        }

        private void SetCooldowDecreasePercent(float percent)
        {
            _cooldownDecrease = percent;
            for (int i = 0; i < _activeWeapons.Count; i++)
            {
                _activeWeapons[i].ChangeCooldownDecrease(_cooldownDecrease);
            }
        }
    }
}
