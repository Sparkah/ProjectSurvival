using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectileMoverToWorldPoint : MonoBehaviour
    {
        [SerializeField] private WeaponProjectile _weaponProjectile;
        [SerializeField] private Vector3 _worldPoint;
        [SerializeField] private Transform _landMark;

        private void Start()
        {
            _landMark.gameObject.SetActive(false);
            _landMark.parent = null;
        }

        private void OnDestroy()
        {
            Destroy(_landMark.gameObject);
        }

        private void OnEnable()
        {
            Refresh();
        }

        private void FixedUpdate()
        {
            Move();
            if (transform.position == _worldPoint)
                Hide();
        }

        private void Refresh() 
        {
            _landMark.position = _worldPoint;
            _landMark.gameObject.SetActive(true);
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position, _worldPoint, _weaponProjectile.Speed * Time.fixedDeltaTime);
        }

        private void Hide()
        {
            _weaponProjectile.ReturnToPool();
            _landMark.gameObject.SetActive(false);
        }
    }
}
