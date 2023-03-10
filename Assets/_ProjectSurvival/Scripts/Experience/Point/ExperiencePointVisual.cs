using UnityEngine;

namespace _ProjectSurvival.Scripts.Experience
{
    [System.Serializable]
    public class ExperiencePointVisual
    {
        [SerializeField] private float _experienceAmount;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;

        public float ExperienceAmount => _experienceAmount;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
}
