using UnityEngine.Events;

public interface IDamagable
{
    public event UnityAction OnDamaged;
    public event UnityAction OnRestored;
    public float MaximumDurability { get; }
    public float CurrentDurability { get; }
    public void TakeDamage(float damageAmount);
    public void RestoreDurability();
}
