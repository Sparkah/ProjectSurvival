using System.Collections;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Types
{
    public class FixedProjectile : WeaponProjectile
    {
        public override void CustomPrepareForLaunch(ProjectileSettings projectileSettings)
        {
            transform.localScale = new Vector3(Size, Size, Size);
        }

        public override void CustomLaunch(Vector3 launchPosition, Vector3 forwardDirection)
        {

        }

        protected override float CalculateDamage()
        {
            return BaseDamage;
        }

        protected override void PrepareForReturningToPool()
        {
            StartCoroutine(ReturnToPool());
        }

        private new IEnumerator ReturnToPool()
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector3.zero;
        }
    }
}
