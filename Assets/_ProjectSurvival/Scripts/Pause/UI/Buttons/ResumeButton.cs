/// <summary>
/// Resume button.
/// </summary>
public class ResumeButton : PauseStateControlButton
{
    protected override void OnClick()
    {
        PauseControllerSO.ResumeGame();
    }
}
