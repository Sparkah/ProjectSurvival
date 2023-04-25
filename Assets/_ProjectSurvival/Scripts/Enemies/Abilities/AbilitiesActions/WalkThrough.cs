using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions
{
    public class WalkThrough : Ability
    {
        public override void ImplementAbility(EnemyAbilities ability)
        {
            if (ability != EnemyAbilities.WalkThrough) return;
            var layer = LayerMask.NameToLayer("EnemyWalkThroughColliders");
            _enemy.gameObject.layer = layer;
            gameObject.layer = layer;
        }
    }
}
