/// <summary>
/// Pause button.
/// </summary>
public class PauseButton : PauseStateControlButton
{
    protected override void OnClick()
    {
        PauseControllerSO.PauseGame(true);
    }
}
