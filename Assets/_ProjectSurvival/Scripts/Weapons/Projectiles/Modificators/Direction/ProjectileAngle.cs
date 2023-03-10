using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public abstract class ProjectileAngle : MonoBehaviour
    {
        public abstract Quaternion CalculateAngle(Vector3 forwardDirection);
    }
}
