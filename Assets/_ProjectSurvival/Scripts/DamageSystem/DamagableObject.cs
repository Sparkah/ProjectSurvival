using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.Effects;
using UnityEngine;
using UnityEngine.Events;

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

    public void TakeDamage(float damageAmount)
    {
        //Add check it is not player, otherwise remove
        PlayEffectsOnHit();
        _health -= damageAmount;
        OnDamaged?.Invoke();
        if (_health <= 0)
        {
            _isDefeated = true;
            OnDefeat?.Invoke();
        }
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
