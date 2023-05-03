using _ProjectSurvival.Scripts.Experience.Point;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions
{
    public class XpDestroyer : Ability
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _radiusIncrease = 1.2f;
        public override void ImplementAbility(EnemyAbilities ability)
        {
            if (ability != EnemyAbilities.XpDestroyer) return;
            var layer = LayerMask.NameToLayer("EnemyXPDestroyers");
            _collider.radius = _radiusIncrease;
            //_enemy.gameObject.layer = layer;
            gameObject.layer = layer;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ExperiencePoint experiencePoint)) return;
            experiencePoint.Collect(gameObject.transform);
        }
    }
}
