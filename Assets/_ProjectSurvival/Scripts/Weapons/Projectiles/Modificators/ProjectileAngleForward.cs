using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectileAngleForward : ProjectileAngle
    {
        public override Quaternion CalculateAngle(Vector3 forwardDirection)
        {
            return Quaternion.LookRotation(forwardDirection);
        }
    }
}
