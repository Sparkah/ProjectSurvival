using UnityEngine;

public class ObjectAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Sprite _frontSprite;
    private Sprite _backSprite;

    public void SetupSprite(Sprite frontSprite, Sprite backSprite)
    {
        _frontSprite = frontSprite;
        _backSprite = backSprite;
    }

    public void ChangeSide(bool isMovingForward, bool isMovingRight)
    {
        if (isMovingForward)
            ChangeSprite(_backSprite);
        else
            ChangeSprite(_frontSprite);
        _spriteRenderer.flipX = isMovingRight;
    }

    private void ChangeSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
