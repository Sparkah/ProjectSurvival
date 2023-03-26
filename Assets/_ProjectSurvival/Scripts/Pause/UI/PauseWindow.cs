using _ProjectSurvival.Scripts.Windows;
using UnityEngine;

/// <summary>
/// Automatic pause window.
/// </summary>
public class PauseWindow : MonoBehaviour
{
    [SerializeField] private PauseControllerSO _pauseControllerSO;
    [SerializeField] private Window _window;

    private void Start()
    {
        _pauseControllerSO.OnGamePausedByRequest += ShowPauseWindow;
        _pauseControllerSO.OnGameResumed += HidePauseWindow;
    }

    private void OnDestroy()
    {
        _pauseControllerSO.ResumeGame();
        _pauseControllerSO.OnGamePausedByRequest -= ShowPauseWindow;
        _pauseControllerSO.OnGameResumed -= HidePauseWindow;
    }

    private void ShowPauseWindow()
    {
        _window.Open();
    }

    private void HidePauseWindow()
    {
        _window.Close();
    }
}
