using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class GameObjectPool<T> : MonoBehaviour where T : Component
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private T _prefab;
    [Tooltip("Collection checks will throw errors if we try to release an item that is already in the pool.")]
    [SerializeField] private bool _collectionChecks = true;
    [SerializeField] private int _maxPoolSize = 10;
    private LinkedPool<T> _pool;
    private bool _isRemoving;

    public LinkedPool<T> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new LinkedPool<T>(
                        CreatePooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        _collectionChecks,
                        _maxPoolSize);
            }
            return _pool;
        }
    }

    public void InitPool(T prefab)
    {
        _prefab = prefab;
    }

    public void MarkForRemoving()
    {
        _isRemoving = true;
        if (IsEmpty(0, 0))
            Remove();
    }

    private T CreatePooledItem()
    {
        T spawned = _diContainer.InstantiatePrefabForComponent<T>(_prefab, transform);
        ((IPoolableObject<T>)spawned).Init(Pool);
        return spawned;
    }

    // Called when an item is returned to the pool using Release
    private void OnReturnedToPool(T returningObject)
    {
        returningObject.gameObject.SetActive(false);
        //Check for (+ 1) because  this check is happening
        //OnReturnedToPool - when returning action is not finished.
        if (_isRemoving && IsEmpty(1, 0))
            Remove();
    }

    // Called when an item is taken from the pool using Get
    private void OnTakeFromPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    private void OnDestroyPoolObject(T pooledObject)
    {
        ((IPoolableObject<T>)pooledObject).Destroy();
        Destroy(pooledObject.gameObject);

        //Check for (- 1) because  this check is happening
        //right after Destroy - but Destroy() is not instant method.
        if (_isRemoving && IsEmpty(0, -1))
            Remove();
    }

    private bool IsEmpty(int countInactiveModifier, int childCountModifier)
    {
        return Pool.CountInactive + countInactiveModifier == transform.childCount + childCountModifier;
    }

    private void Remove()
    {
        _isRemoving = false;
        Pool.Clear();
        Pool.Dispose();
        Destroy(gameObject);
    }
}
