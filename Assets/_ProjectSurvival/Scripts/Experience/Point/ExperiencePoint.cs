using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.Enemies.Types;
using _ProjectSurvival.Scripts.Pool;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace _ProjectSurvival.Scripts.Experience.Point
{
    public class ExperiencePoint : MonoBehaviour, IPoolableObject<ExperiencePoint>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ExperiencePointSettingsSO _settingsSO;
        [SerializeField] private ExperienceVisualTableSO _experienceVisualTableSO;
        private IObjectPool<ExperiencePoint> _pool;
        private EnemyTypeSO _enemyTypeSO;
        private float _experienceAmount;
        private bool _isCollected;
        private Quaternion _defaultRotation;

        public float ExperienceAmount => _experienceAmount;
        public EnemyTypeSO EnemyTypeSO => _enemyTypeSO;

        private void OnDestroy()
        {
            if (_spriteRenderer!=null)
                _spriteRenderer.transform.DOKill();
        }

        public void Init(IObjectPool<ExperiencePoint> pool)
        {
            _pool = pool;
            _defaultRotation = _spriteRenderer.transform.rotation;
        }

        public void Destroy()
        {
            _spriteRenderer.transform.DOKill();
        }

        public void ReturnToPool()
        {
            AudioPlayer.Audio.PlaySound(AudioSounds.Coins);
            _spriteRenderer.transform.DOKill();
            _pool.Release(this);
        }

        public void Drop(Vector3 position, EnemyTypeSO enemyTypeSO, float experienceAmount)
        {
            _enemyTypeSO = enemyTypeSO;
            _experienceAmount = experienceAmount;
            UpdateVisual(experienceAmount);
            transform.position = position;
            _isCollected = false;
        }

        public bool Collect(Transform collectorTransform)
        {
            if (!_isCollected)
            {
                _isCollected = true;
                MoveToCollector(collectorTransform).Forget();
                _spriteRenderer.transform
                    .DOLocalRotate(new Vector3(0, 0, 360), _settingsSO.RotatingSpeed, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);
                return true;
            }
            return false;
        }

        private async UniTaskVoid MoveToCollector(Transform collectorTransform)
        {
            while (!IsTargetReached(collectorTransform.position))
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    collectorTransform.position,
                    _settingsSO.GatheringSpeed * Time.deltaTime);
                await UniTask.NextFrame(cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            ReturnToPool();
        }

        private void UpdateVisual(float experienceAmount)
        {
            ExperiencePointVisual experiencePointVisual =
                _experienceVisualTableSO.GetVisualForExperienceAmount(experienceAmount);
            _spriteRenderer.sprite = experiencePointVisual.Sprite;

            _spriteRenderer.color = _enemyTypeSO.TypeColor;
            _spriteRenderer.transform.rotation = _defaultRotation;
        }

        private bool IsTargetReached(Vector3 targetPosition)
        {
            return (targetPosition - transform.position).sqrMagnitude < _settingsSO.ApproximatelyDistance;
        }
    }
}
