using UnityEngine;
using UnityEngine.Events;

public class DamagableObject : MonoBehaviour, IDamagable
{
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
        OnRestored?.Invoke();
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;
        OnDamaged?.Invoke();
        if (_health <= 0)
        {
            _isDefeated = true;
            OnDefeat?.Invoke();
        }
    }
}
