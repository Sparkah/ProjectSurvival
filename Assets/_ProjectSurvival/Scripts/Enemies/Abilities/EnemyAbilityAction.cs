using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities
{
    public class EnemyAbilityAction : MonoBehaviour
    {
        [SerializeField] private Ability[] _abilities;

        public void SetUpAbility(EnemyAbilities abilities)
        {
            switch (abilities)
            {
                case EnemyAbilities.WalkThrough:
                    Debug.Log("Walk through");
                    break;
                case EnemyAbilities.WaveDamager:
                    Debug.Log("Wave Damager");
                    break;
                case EnemyAbilities.XpDestroyer:
                    Debug.Log("xp destroyer");
                    break;
                default:
                    Debug.Log("no ability");
                    break;
            }
        }
    }
}