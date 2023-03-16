using UnityEngine;

/// <summary>
/// Controls links from selected object's buttons to another windows.
/// </summary>
public class NavigationHub : MonoBehaviour
{
    [SerializeField] private OpenWindowButton[] _navigationLinks;

    private void Start()
    {
        for (int i = 0; i < _navigationLinks.Length; i++)
            _navigationLinks[i].Init();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _navigationLinks.Length; i++)
            _navigationLinks[i].Clear();
    }
}
