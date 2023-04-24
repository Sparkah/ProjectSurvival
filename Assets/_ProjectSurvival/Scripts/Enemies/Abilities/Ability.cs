using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities
{
    public class Ability : MonoBehaviour
    {
        protected EnemyAbilityAction _enemy;
        public virtual void ImplementAbility(EnemyAbilities ability)
        {
            
        }

        public void Construct(EnemyAbilityAction enemy)
        {
            _enemy = enemy;
        }
    }
}
