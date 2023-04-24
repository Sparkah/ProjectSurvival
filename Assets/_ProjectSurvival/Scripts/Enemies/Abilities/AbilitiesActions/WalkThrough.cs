using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions
{
    public class WalkThrough : Ability
    {
        public override void ImplementAbility(EnemyAbilities ability)
        {
            if (ability != EnemyAbilities.WalkThrough) return;
            Debug.Log(ability + " implemented in Walk through child class");
            var layer = LayerMask.NameToLayer("EnemyWalkThroughColliders");
            _enemy.gameObject.layer = layer;
            gameObject.layer = layer;
        }
    }
}
