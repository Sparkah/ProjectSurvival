using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectileAngleRandomizer : ProjectileAngle
    {
        [SerializeField] private float _minDirectionAngle;
        [SerializeField] private float _maxDirectionAngle;

        public override Quaternion CalculateAngle(Vector3 forwardDirection)
        {
            float customAngle = Random.Range(_minDirectionAngle, _maxDirectionAngle);
            Quaternion forwardRotation = Quaternion.LookRotation(forwardDirection);
            return forwardRotation * Quaternion.Euler(Vector3.right * customAngle);
        }
    }
}
