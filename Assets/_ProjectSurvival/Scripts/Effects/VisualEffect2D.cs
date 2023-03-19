using UnityEngine;

namespace _ProjectSurvival.Scripts.Effects
{
    public abstract class VisualEffect2D : MonoBehaviour, IEffect
    {
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private float _duration;

        protected SpriteRenderer[] SpriteRenderers => _spriteRenderers;

        protected abstract void ShowEffect(float duration);
        public abstract void ResetEffect();

        public void PlayEffect()
        {
            ShowEffect(_duration);
        }
    }
}
