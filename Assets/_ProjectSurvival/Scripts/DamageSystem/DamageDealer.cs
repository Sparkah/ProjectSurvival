using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.DamageSystem
{
    public class DamageDealer : MonoBehaviour
    {
        public event UnityAction<IDamagable> OnDamagableTouched;
        public event UnityAction OnDestructionTouched;
        
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _destructionMask;
        private LayerMask _playerLayer;

        private void Awake()
        {
            int playerLayerIndex = LayerMask.NameToLayer("PlayerCollider");
            _playerLayer = 1 << playerLayerIndex;
        }

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

            int otherLayerMask = 1 << other.gameObject.layer;
    
            if (_playerLayer.value == otherLayerMask && (_targetLayer.value & otherLayerMask) != 0)
            {
                AudioPlayer.Audio.PlayOneShotSound(AudioSounds.Hit);
            }
        }
    }
}
