using UnityEngine.Pool;

namespace _ProjectSurvival.Scripts.Pool
{
    public interface IPoolableObject<T> where T : class
    {
        public void Init(IObjectPool<T> pool);
        public void Destroy();
        public void ReturnToPool();
    }
}
