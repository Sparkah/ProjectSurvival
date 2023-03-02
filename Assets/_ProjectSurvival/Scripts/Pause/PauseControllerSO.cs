using UnityEngine;

/// <summary>
/// Controls game pause state.
/// </summary>
[CreateAssetMenu(fileName = "Pause controller", menuName = "Core/Game flow/Pause controller", order = 1)]
public class PauseControllerSO : ScriptableObject
{
    public event System.Action OnGamePaused;
    public event System.Action OnGamePausedByRequest;
    public event System.Action OnGameResumed;

    /// <summary>
    /// Pause game.
    /// </summary>
    public void PauseGame(bool isPlayerRequest = false)
    {
        Time.timeScale = 0f;
        if (!isPlayerRequest)
            OnGamePaused?.Invoke();
        else
            OnGamePausedByRequest?.Invoke();
    }

    /// <summary>
    /// Resume game.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
    }
}
