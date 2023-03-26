using _ProjectSurvival.Scripts.Windows;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button that opens selected window.
/// </summary>
[System.Serializable]
public class OpenWindowButton
{
    [SerializeField] private Window _window;
    [SerializeField] private Button _button;

    /// <summary>
    /// Init button.
    /// </summary>
    public void Init()
    {
        _button.onClick.AddListener(OpenWindow);
    }

    /// <summary>
    /// Clear button's connections.
    /// </summary>
    public void Clear()
    {
        _button.onClick.RemoveListener(OpenWindow);
    }

    private void OpenWindow()
    {
        _window.Open();
    }
}
