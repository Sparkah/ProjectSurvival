using UnityEngine;

/// <summary>
/// Window that is represented using Canvas.
/// </summary>
public class CanvasWindow : Window
{
    [SerializeField] private Canvas _canvas;

    protected override void HandleClose()
    {
        _canvas.enabled = false;
    }

    protected override void HandleOpen()
    {
        _canvas.enabled = true;
    }
}
