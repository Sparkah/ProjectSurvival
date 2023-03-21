using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    [SerializeField] private int _targetFps = 30;

    private void Start()
    {
        if (Application.targetFrameRate != _targetFps)
            Application.targetFrameRate = _targetFps;
    }
}
