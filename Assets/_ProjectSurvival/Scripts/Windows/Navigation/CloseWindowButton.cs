using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.Windows;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button that closes selected window.
/// </summary>
public class CloseWindowButton : MonoBehaviour
{
    [SerializeField] private Window _window;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(CloseWindow);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(CloseWindow);
    }

    private void CloseWindow()
    {
        _window.Close();
    }
}
