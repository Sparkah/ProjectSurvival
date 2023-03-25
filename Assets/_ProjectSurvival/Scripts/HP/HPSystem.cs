using _ProjectSurvival.Scripts.GameFlow.SessionOver;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.HP
{
    public class HPSystem : MonoBehaviour
    {
        [Inject] private SessionOverController _sessionOverController;
        [SerializeField] private DamagableObject _damagableObject;
        [SerializeField] private Image _healthSlider;

        private void Awake()
        {
            _damagableObject.OnDamaged += UpdateUI;
        }

        private void OnDestroy()
        {
            _damagableObject.OnDamaged -= UpdateUI;
        }

        private void UpdateUI()
        {
            _healthSlider.fillAmount = _damagableObject.CurrentDurability / _damagableObject.MaximumDurability;
            
            if (_healthSlider.fillAmount <= 0)
            {
                _sessionOverController.EndSession(SessionResult.Died);
            }
        }
    }
}