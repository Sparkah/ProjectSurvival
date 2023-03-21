using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
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

        public event UnityAction<ActiveWeapon> OnFire;

        private void OnDestroy()
        {
            for (int i = 0; i < _activeWeapons.Count; i++)
            {
                _activeWeapons[i].OnFire -= RequestFire;
                _activeWeapons[i].CancelFiring();
            }
        }

        public void GiveReward(IReward reward)
        {
            if (reward.GetRewardType() == RewardType.Weapon)
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

        private void AddExtraLevelUpFromSkillUpgrades(ActiveWeapon selectedWeapon, WeaponTypeSO weaponType)
        {
            _upgradesQue.TryGetValue(weaponType, out int quedAmount);
            if (quedAmount <= 0) return;
            for (var i = 0; i < quedAmount; i++)
            {
                selectedWeapon.LevelUp();
//                    Debug.Log("levelling up weapon from upgrades que");
            }
        }

        public bool HasMaximumLevel(WeaponTypeSO weaponType)
        {
            ActiveWeapon selectedWeapon = FindWeapon(weaponType);
            if (selectedWeapon == null)
                return false;

            return selectedWeapon.HasMaximumLevel();
        }

        private ActiveWeapon FindWeapon(WeaponTypeSO weaponType)
        {
            return _activeWeapons.Find(x => x.WeaponType == weaponType);
        }

        private void RequestFire(ActiveWeapon activeWeapon)
        {
            OnFire?.Invoke(activeWeapon);
        }
    }
}
