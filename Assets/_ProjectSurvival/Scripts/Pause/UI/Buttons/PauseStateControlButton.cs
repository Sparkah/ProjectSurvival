using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button for controlling pause state.
/// </summary>
public abstract class PauseStateControlButton : MonoBehaviour
{
    [SerializeField] private PauseControllerSO _pauseControllerSO;
    [SerializeField] private Button _button;

    /// <summary>
    /// Pause controller reference.
    /// </summary>
    protected PauseControllerSO PauseControllerSO => _pauseControllerSO;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    /// <summary>
    /// Custom affect to game pause state.
    /// </summary>
    protected abstract void OnClick();
}
