using _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions;
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
        // Your variable declarations...
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ExperiencePointSettingsSO _settingsSO;
        [SerializeField] private ExperienceVisualTableSO _experienceVisualTableSO;
        private IObjectPool<ExperiencePoint> _pool;
        private EnemyTypeSO _enemyTypeSO;
        private float _experienceAmount;
        private bool _isCollected;
        private Quaternion _defaultRotation;
        private Tween _rotationTween;
        private bool _returnedToPool;
        private XpDestroyer _xpDestroyer;

        public float ExperienceAmount => _experienceAmount;
        public EnemyTypeSO EnemyTypeSO => _enemyTypeSO;
    
        public void Init(IObjectPool<ExperiencePoint> pool)
        {
            _pool = pool;
            _defaultRotation = _spriteRenderer.transform.rotation;
            _spriteRenderer?.transform.DOKill();
        }

        public void Destroy()
        {
            UnsubscribeFromEvents();
            _spriteRenderer?.transform.DOKill();
            if (this != null && !_returnedToPool)
            {
                _returnedToPool = true;
                _pool.Release(this);
            }
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void OnEnable()
        {
            _returnedToPool = false;
        }

        public void ReturnToPool()
        {
            Destroy();
        }

        public bool Collect(Transform collectorTransform)
        {
            if (!_isCollected && collectorTransform != null)
            {
                _isCollected = true;
                _rotationTween = _spriteRenderer.transform
                    .DOLocalRotate(new Vector3(0, 0, 360), _settingsSO.RotatingSpeed, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);

                SubscribeToEvents(collectorTransform);

                MoveToCollector(collectorTransform).Forget();

                return true;
            }
            return false;
        }

        private void SubscribeToEvents(Transform collectorTransform)
        {
            XpDestroyer xpDestroyer;
            if (collectorTransform != null && collectorTransform.TryGetComponent(out xpDestroyer))
            {
                _xpDestroyer = xpDestroyer;
                if (_xpDestroyer.DamagableObject != null)
                {
                    _xpDestroyer.DamagableObject.OnDefeat += UpdateDamagableDeathState;
                }
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (_xpDestroyer != null && _xpDestroyer.DamagableObject != null)
            {
                _xpDestroyer.DamagableObject.OnDefeat -= UpdateDamagableDeathState;
            }
        }

        private void UpdateDamagableDeathState()
        {
            _rotationTween.Kill();
            ReturnToPool();
        }

        private async UniTaskVoid MoveToCollector(Transform collectorTransform)
        {
            if (collectorTransform == null) return;

            Vector3 targetPosition = collectorTransform.position;
            while (!_returnedToPool)
            {
                // If _xpDestroyer becomes null, continue moving towards the last known position
                if (_xpDestroyer == null)
                {
                    //Debug.Log("Collector destroyed, continuing to last known position");
                    collectorTransform = null;  // Don't update targetPosition anymore
                }
                else if(collectorTransform != null)
                {
                    targetPosition = collectorTransform.position;
                }

                if (IsTargetReached(targetPosition))
                {
                    ReturnToPool();
                    break;
                }

                MoveTowardsCollector(targetPosition);
                await UniTask.NextFrame(cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }

        private void MoveTowardsCollector(Vector3 collectorPosition)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                collectorPosition,
                _settingsSO.GatheringSpeed * Time.deltaTime);
        }

        private bool IsTargetReached(Vector3 targetPosition)
        {
            return (targetPosition - transform.position).sqrMagnitude < _settingsSO.ApproximatelyDistance;
        }
        
        public void Drop(Vector3 position, EnemyTypeSO enemyTypeSO, float experienceAmount)
        {
            _enemyTypeSO = enemyTypeSO;
            _experienceAmount = experienceAmount;
            UpdateVisual(experienceAmount);
            transform.position = position;
            _isCollected = false;
        }
        
        private void UpdateVisual(float experienceAmount)
        {
            ExperiencePointVisual experiencePointVisual =
                _experienceVisualTableSO.GetVisualForExperienceAmount(experienceAmount);
            _spriteRenderer.sprite = experiencePointVisual.Sprite;

            _spriteRenderer.color = _enemyTypeSO.TypeColor;
            _spriteRenderer.transform.rotation = _defaultRotation;
        }
    }
}
