using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectileAngleToWorldPoint : ProjectileAngle
    {
        [SerializeField] private Vector3 _worldPoint;
        public override Quaternion CalculateAngle(Vector3 forwardDirection)
        {
            return Quaternion.LookRotation((_worldPoint - transform.position).normalized);
        }
    }
}
