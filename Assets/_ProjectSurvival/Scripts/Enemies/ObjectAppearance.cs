using UnityEngine;

public class ObjectAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetupSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
