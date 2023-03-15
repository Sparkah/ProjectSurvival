using _ProjectSurvival.Scripts.Helpers;
using _ProjectSurvival.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.HP
{
    public class HPSystem : MonoBehaviour
    {
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
            _healthSlider.fillAmount = _damagableObject.CurrentDurability/_damagableObject.MaximumDurability;

            if (_healthSlider.fillAmount <= 0)
            {
                Debug.Log("Game over");
                LevelLoader levelLoader = new LevelLoader();
                levelLoader.ReloadCurrentLevel();
            }
        }
    }
}