using System;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions
{
    public class WalkThrough : Ability
    {
        [SerializeField] private SpriteRenderer _renderer;
        
        public override void ImplementAbility(EnemyAbilities ability)
        {
            if (ability != EnemyAbilities.WalkThrough) return;
            var layer = LayerMask.NameToLayer("EnemyWalkThroughColliders");
            _enemy.gameObject.layer = layer;
            gameObject.layer = layer;
        }

        public void ChangeSpriteVisibility(float alpha)
        {
            //Debug.Log("alpha changed");
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
        }

        private void OnDisable()
        {
            ChangeSpriteVisibility(1);
        }

        private void OnEnable()
        {
            ChangeSpriteVisibility(1);
        }
    }
}
