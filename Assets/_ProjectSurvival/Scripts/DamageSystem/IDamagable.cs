using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.DamageSystem
{
    public interface IDamagable
    {
        public event UnityAction OnDamaged;
        public event UnityAction OnRestored;
        public float MaximumDurability { get; }
        public float CurrentDurability { get; }
        /// <summary>
        /// Additional check for objects - if they were defeated at same fixed update in previous collision.
        /// </summary>
        public bool IsDefeated { get; }
        public void TakeDamage(float damageAmount);
        public void RestoreDurability();
    }
}
