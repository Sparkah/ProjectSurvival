using UnityEngine;

public class AngledProjectile : ForwardProjectlie
{
    [SerializeField] private float _directionAngle;

    protected override Quaternion CalculateRotation(Vector3 forwardDirection)
    {
        float customAngle = Random.Range(-_directionAngle, _directionAngle);
        Quaternion forwardRotation = Quaternion.LookRotation(forwardDirection);
        return forwardRotation * Quaternion.Euler(Vector3.right * customAngle);
    }
}
