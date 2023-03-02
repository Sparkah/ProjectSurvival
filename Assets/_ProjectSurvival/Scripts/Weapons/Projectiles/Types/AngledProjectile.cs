using UnityEngine;

public class AngledProjectile : ForwardProjectlie
{
    [SerializeField] private float _directionAngle;

    protected override Quaternion CalculateRotation(Vector3 forwardDirection)
    {
        float customAngle = Random.Range(-_directionAngle, _directionAngle);
        return Quaternion.Euler(0, 0, customAngle);
    }
}
