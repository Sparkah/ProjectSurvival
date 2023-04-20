using _ProjectSurvival.Scripts.DamageSystem;
using UnityEngine;
using UnityEngine.UI;

public class DamagableUI : MonoBehaviour
{
    [SerializeField] private Slider _durabilitySlider;
    [SerializeField] private Behaviour _damagableObject;
    private IDamagable _damagable => _damagableObject as IDamagable;

    private void Start()
    {
        SetupUI();
        _damagable.OnDamaged += UpdateUI;
        _damagable.OnRestored += SetupUI;
    }

    private void OnDestroy()
    {
        _damagable.OnDamaged -= UpdateUI;
        _damagable.OnRestored -= SetupUI;
    }

    private void SetupUI()
    {
        _durabilitySlider.maxValue = _damagable.MaximumDurability;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _durabilitySlider.value = _damagable.CurrentDurability;
    }
}
