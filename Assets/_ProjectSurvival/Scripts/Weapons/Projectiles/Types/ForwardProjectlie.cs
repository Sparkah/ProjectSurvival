using UnityEngine;

public class ForwardProjectlie : WeaponProjectile
{
    private float _speed;
    private float _baseDamage;

    private void FixedUpdate()
    {
        transform.position += transform.up * _speed * Time.fixedDeltaTime;
    }

    public override void PrepareForLaunch(ProjectileSettings projectileSettings)
    {
        _baseDamage = projectileSettings.Damage;
        _speed = projectileSettings.Speed;
        SetDurability(projectileSettings.Durability);
        transform.localScale = new Vector3(projectileSettings.Size, projectileSettings.Size, projectileSettings.Size);
    }

    public override void Launch(Vector3 launchPosition, Vector3 forwardDirection)
    {
        transform.position = launchPosition;
        transform.rotation = CalculateRotation(forwardDirection);
    }

    protected virtual Quaternion CalculateRotation(Vector3 forwardDirection)
    {
         return Quaternion.identity;
    }

    protected override float CalculateDamage()
    {
        return _baseDamage;
    }

    protected override void PrepareForReturningToPool()
    {
        transform.position = Vector3.zero;
    }
}
