using _ProjectSurvival.Scripts.Enemies;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace _ProjectSurvival.Scripts.Experience
{
    public class ExperiencePoint : MonoBehaviour, IPoolableObject<ExperiencePoint>
    {
        [SerializeField] private float _gatheringSpeed;
        [SerializeField] private float _rotatingSpeed;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ExperienceVisualTableSO _experienceVisualTableSO;
        private IObjectPool<ExperiencePoint> _pool;
        private EnemyTypeSO _enemyTypeSO;
        private float _experienceAmount;
        private bool _isCollected;
        private Quaternion _defaultRotation;

        public float ExperienceAmount => _experienceAmount;
        public EnemyTypeSO EnemyTypeSO => _enemyTypeSO;

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
                    .DOLocalRotate(new Vector3(0, 0, 360), _rotatingSpeed, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);
                return true;
            }
            return false;
        }

        private async UniTaskVoid MoveToCollector(Transform collectorTransform)
        {
            while (transform.position != collectorTransform.position)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    collectorTransform.position,
                    _gatheringSpeed * Time.deltaTime);
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
    }
}
