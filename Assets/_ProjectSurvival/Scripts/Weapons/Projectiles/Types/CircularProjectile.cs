using _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators.Movement;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Types
{
    public class CircularProjectile : WeaponProjectile
    {
        [SerializeField] private ProjectileCircularMover _circularMover;
        public override void CustomPrepareForLaunch(ProjectileSettings projectileSettings)
        {
            transform.localScale = new Vector3(Size, Size, Size);
        }

        public override void CustomLaunch(Vector3 launchPosition, Vector3 forwardDirection)
        {
        }

        public void SetUpProjectiles(int id, int arrayLength)
        {
            _circularMover.SetUpProjectiles(id, arrayLength);
        }

        protected override float CalculateDamage()
        {
            return BaseDamage;
        }

        protected override void PrepareForReturningToPool()
        {
            transform.position = Vector3.zero;
        }
    }
}