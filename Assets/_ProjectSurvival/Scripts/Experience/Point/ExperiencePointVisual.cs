using UnityEngine;

namespace _ProjectSurvival.Scripts.Experience
{
    [System.Serializable]
    public class ExperiencePointVisual
    {
        [SerializeField] private float _experienceAmount;
        [SerializeField] private Sprite _sprite;

        public float ExperienceAmount => _experienceAmount;
        public Sprite Sprite => _sprite;
    }
}
