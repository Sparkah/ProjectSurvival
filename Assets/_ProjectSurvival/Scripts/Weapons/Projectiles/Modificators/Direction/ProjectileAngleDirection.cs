using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectileAngleDirection : ProjectileAngle
    {
        [SerializeField] private ProjectileDirection _direction;
        public override Quaternion CalculateAngle(Vector3 forwardDirection)
        {
            Vector3 direction = forwardDirection;
            switch (_direction)
            {
                case ProjectileDirection.Forward:
                    direction = forwardDirection;
                    break;
                case ProjectileDirection.Backward:
                    direction = -forwardDirection;
                    break;
            }
            return Quaternion.LookRotation(direction);
        }
    }
}
