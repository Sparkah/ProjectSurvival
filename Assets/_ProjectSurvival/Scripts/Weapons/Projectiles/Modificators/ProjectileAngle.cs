using UnityEngine;

public abstract class ProjectileAngle : MonoBehaviour
{
    public abstract Quaternion CalculateAngle(Vector3 forwardDirection);
}
