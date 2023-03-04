using UnityEngine;

public class ProjectileAngleRandomizer : ProjectileAngle
{
    [SerializeField] private float _directionAngle;

    public override Quaternion CalculateAngle(Vector3 forwardDirection)
    {
        float customAngle = Random.Range(-_directionAngle, _directionAngle);
        Quaternion forwardRotation = Quaternion.LookRotation(forwardDirection);
        return forwardRotation * Quaternion.Euler(Vector3.right * customAngle);
    }
}
