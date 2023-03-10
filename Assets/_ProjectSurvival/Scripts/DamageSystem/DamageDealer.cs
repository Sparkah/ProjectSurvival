using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    public event UnityAction<IDamagable> OnDamagableTouched;
    public event UnityAction OnNotDamagableTouched;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy || !other.attachedRigidbody)
            return;

        Debug.Log($"damage Dealer on +{other.name}");
        GameObject touchedObject = other.attachedRigidbody.gameObject;
        bool isTarget = _targetLayer.Contains(touchedObject.layer);

        if (isTarget && touchedObject.TryGetComponent(out IDamagable damagedObject))
        {
            Debug.Log("damagable component is present");
            if (!damagedObject.IsDefeated)
            {
                OnDamagableTouched?.Invoke(damagedObject);
            }
        }
        else
        {
            OnNotDamagableTouched?.Invoke();
        }
    }
}
