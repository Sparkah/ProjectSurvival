using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    [System.Serializable]
    public class ProjectileSettings
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _size;
        [SerializeField] private float _speed;
        [SerializeField] private int _durability;

        public float Damage => _damage;
        public float Size => _size;
        public float Speed => _speed;
        public int Durability => _durability;
    }
}
