using System.Collections.Generic;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ActiveWeapons : MonoBehaviour, IRewardGiver
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private ProjectilesPool _projectilePoolPrefab;
    private List<ActiveWeapon> _activeWeapons = new List<ActiveWeapon>();

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
        if(reward.GetRewardType()==RewardType.Weapon)
            AddWeapon((WeaponTypeSO)reward);
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
