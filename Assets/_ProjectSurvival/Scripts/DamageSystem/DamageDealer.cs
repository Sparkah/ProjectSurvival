using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.DamageSystem
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _destructionMask;

        public event UnityAction<IDamagable> OnDamagableTouched;
        public event UnityAction OnDestructionTouched;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!gameObject.activeInHierarchy || !other.attachedRigidbody)
                return;
        
            GameObject touchedObject = other.attachedRigidbody.gameObject;

            if (_destructionMask.Contains(touchedObject.layer))
            {
                OnDestructionTouched?.Invoke();
                return;
            }

            bool isTarget = _targetLayer.Contains(touchedObject.layer);
            if (isTarget && touchedObject.TryGetComponent(out IDamagable damagedObject))
            {
                if (!damagedObject.IsDefeated)
                {
                    OnDamagableTouched?.Invoke(damagedObject);
                }
            }
        
            if (_targetLayer.value == 256)
            {
                AudioPlayer.Audio.PlaySound(AudioSounds.Hit);
            }
        }
    }
}
