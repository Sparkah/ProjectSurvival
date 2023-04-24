using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities
{
    public class EnemyAbilityAction : MonoBehaviour
    {
        [SerializeField] private Ability[] _abilities;

        public void SetUpAbility(EnemyAbilities abilities)
        {
            ImplementAbilities(abilities);
        }

        private void ImplementAbilities(EnemyAbilities abilities)
        {
            foreach (var ability in _abilities)
            {
                ability.Construct(this);
                ability.ImplementAbility(abilities);
            }
        }
    }
}