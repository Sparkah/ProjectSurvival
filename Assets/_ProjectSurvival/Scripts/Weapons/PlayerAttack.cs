using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    [Inject] private ActiveWeapons _activeWeapons;
    [SerializeField] private Transform _firePoint;

    public void StartFire()
    {
        _activeWeapons.OnFire += Fire;
    }

    private void OnDestroy()
    {
        _activeWeapons.OnFire -= Fire;
    }

    private void Fire(ActiveWeapon activeWeapon)
    {
        int attackLevel = activeWeapon.Level;
        int projectilesCount = activeWeapon.WeaponType.GetAppearAmount(attackLevel);
        WeaponProjectile attackProjectile;
        for (int i = 0; i < projectilesCount; i++)
        {
            attackProjectile = activeWeapon.GetProjectile();
            attackProjectile.PrepareForLaunch(activeWeapon.WeaponType.GetProjectileSettings(attackLevel));
            attackProjectile.Launch(_firePoint.position, transform.up);
        }
    }
}
