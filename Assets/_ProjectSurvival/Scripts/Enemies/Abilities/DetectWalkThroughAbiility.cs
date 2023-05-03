using _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities
{
    public class DetectWalkThroughAbiility : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyWalkThroughColliders"))
            {
                if (col.TryGetComponent(out WalkThrough walkThrough))
                {
                    walkThrough.ChangeSpriteVisibility(0.5f);
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyWalkThroughColliders"))
            {
                if (col.TryGetComponent(out WalkThrough walkThrough))
                {
                    walkThrough.ChangeSpriteVisibility(1f);
                }
            }
        }
    }
}
