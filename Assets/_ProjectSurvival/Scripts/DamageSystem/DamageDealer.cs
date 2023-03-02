using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    public event UnityAction<IDamagable> OnDamagableTouched;
    public event UnityAction OnNotDamagableTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody)
            return;

        GameObject touchedObject = other.attachedRigidbody.gameObject;
        bool isTarget = _targetLayer.Contains(touchedObject.layer);

        if (isTarget && touchedObject.TryGetComponent(out IDamagable damagedObject))
            OnDamagableTouched?.Invoke(damagedObject);
        else
            OnNotDamagableTouched?.Invoke();
    }
}
