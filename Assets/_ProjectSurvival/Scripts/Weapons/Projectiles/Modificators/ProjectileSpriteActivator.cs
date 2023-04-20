using System.Collections;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators
{
    public class ProjectileSpriteActivator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        private void OnEnable()
        {
            StartCoroutine(EnableSprite());
        }

        private IEnumerator EnableSprite()
        {
            yield return new WaitForEndOfFrame();
            _sprite.enabled = true;
        }

        private void OnDisable()
        {
            _sprite.enabled = false;
        }
    }
}