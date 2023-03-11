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
            HideLandMark();
            _landMark.parent = null;
        }

        private void OnDestroy()
        {
            if (_landMark != null)
                Destroy(_landMark.gameObject);
        }

        private void OnEnable()
        {
            RefreshLandMark();
        }

        private void OnDisable()
        {
            HideLandMark();
        }

        private void FixedUpdate()
        {
            Move();
            if (transform.position == _worldPoint)
            {
                HideLandMark();
                _weaponProjectile.ReturnToPool();
            }
        }

        private void RefreshLandMark() 
        {
            _landMark.position = _worldPoint;
            _landMark.rotation = Quaternion.identity;
            _landMark.gameObject.SetActive(true);
        }

        private void HideLandMark()
        {
            _landMark.gameObject.SetActive(false);
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position, _worldPoint, _weaponProjectile.Speed * Time.fixedDeltaTime);
        }
    }
}
