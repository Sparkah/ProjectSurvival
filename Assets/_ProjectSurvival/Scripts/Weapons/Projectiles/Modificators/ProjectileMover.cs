using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private WeaponProjectile _weaponProjectile;

    private void FixedUpdate()
    {
        transform.position += transform.forward * _weaponProjectile.Speed * Time.fixedDeltaTime;
    }
}
