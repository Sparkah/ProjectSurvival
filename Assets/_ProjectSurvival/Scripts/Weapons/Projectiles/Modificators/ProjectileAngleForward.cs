using UnityEngine;

public class ProjectileAngleForward : ProjectileAngle
{
    public override Quaternion CalculateAngle(Vector3 forwardDirection)
    {
        return Quaternion.LookRotation(forwardDirection);
    }
}
