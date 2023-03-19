using DG.Tweening;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Effects
{
    public class ColorTintEffect : VisualEffect2D
    {
        [SerializeField] private Color _tintColor;
        private Color[] _defaultColors;

        private void Start()
        {
            if (_defaultColors == null)
                DefineDefaultColors();
        }

        private void OnDestroy()
        {
            ResetEffect();
        }

        public override void ResetEffect()
        {
            ResetColors();
            for (int i = 0; i < SpriteRenderers.Length; i++)
                SpriteRenderers[i].DOKill();
        }

        protected override void ShowEffect(float duration)
        {
            ResetColors();
            for (int i = 0; i < SpriteRenderers.Length; i++)
            {
                SpriteRenderers[i].DOKill();
                SpriteRenderers[i].DOColor(_tintColor, duration * 0.5f)
                    .SetEase(Ease.Linear)
                    .SetLoops(2, LoopType.Yoyo);
            }
        }

        private void ResetColors()
        {
            if (_defaultColors == null)
                DefineDefaultColors();
            for (int i = 0; i < SpriteRenderers.Length; i++)
                SpriteRenderers[i].color = _defaultColors[i];
        }

        private void DefineDefaultColors()
        {
            _defaultColors = new Color[SpriteRenderers.Length];
            for (int i = 0; i < SpriteRenderers.Length; i++)
                _defaultColors[i] = SpriteRenderers[i].color;
        }
    }
}
