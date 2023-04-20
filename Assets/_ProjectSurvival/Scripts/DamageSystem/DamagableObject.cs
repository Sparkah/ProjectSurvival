using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.DamageSystem
{
    public class DamagableObject : MonoBehaviour, IDamagable
    {
        [SerializeField] private Behaviour[] _effectsObjects;
        private float _maxHealth;
        private float _health;
        private bool _isDefeated;

        public float MaximumDurability => _maxHealth;
        public float CurrentDurability => _health;
        public bool IsDefeated => _isDefeated;

        public event UnityAction OnDamaged;
        public event UnityAction OnRestored;
        public event UnityAction OnDefeat;
        public event UnityAction<float> OnDamageAmount;

        public void SetupHealth(float health)
        {
            _maxHealth = health;
            RestoreDurability();
        }

        public void RestoreDurability()
        {
            _health = _maxHealth;
            _isDefeated = false;
            ResetEffects();
            OnRestored?.Invoke();
        }

        public void IncreaseMaxHealth(float health)
        {
            var currentHealth = _maxHealth;
            _maxHealth = health;
            _health += _maxHealth - currentHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            PlayEffectsOnHit();
            _health -= damageAmount;
            OnDamaged?.Invoke();
            OnDamageAmount?.Invoke(damageAmount);
            if (_health <= 0)
            {
                _isDefeated = true;
                OnDefeat?.Invoke();
            }
        }

        public void RestoreHP(float healAmount)
        {
            _health += healAmount;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            OnDamaged?.Invoke();
        }

        private void PlayEffectsOnHit()
        {
            AudioPlayer.Audio.PlayOneShotSound(AudioSounds.Enter); //TODO: Remake as effect
            for (int i = 0; i < _effectsObjects.Length; i++)
            {
                (_effectsObjects[i] as IEffect).PlayEffect();
            }
        }

        private void ResetEffects()
        {
            for (int i = 0; i < _effectsObjects.Length; i++)
                (_effectsObjects[i] as IEffect).ResetEffect();
        }
    }
}
