using _ProjectSurvival.Scripts.LevelingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.Enemies.Evolution.UI
{
    public class EvolutionBarUI : MonoBehaviour
    {
        [SerializeField] private Image _enemyImage;
        [SerializeField] private Image _experienceImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _lockedEnemySprite;
        [SerializeField] private Color _unlockedColor;
        private EnemyTypeSO _enemyTypeSO;
        private ILevelable _levelable;

        private void OnDestroy()
        {
            if (_levelable != null)
            {
                _levelable.OnExperienceChanged -= UpdateExperienceUI;
                _levelable.OnLevelUp -= UpdateUI;
            }
        }

        public void Init()
        {
            _enemyImage.sprite = _lockedEnemySprite;
            _experienceImage.fillAmount = 0;
        }

        public void Init(EnemyTypeSO enemyType, ILevelable levelable)
        {
            _enemyTypeSO = enemyType;
            _levelable = levelable;
            UpdateUI();
            _levelable.OnExperienceChanged += UpdateExperienceUI;
            _levelable.OnLevelUp += UpdateUI;
        }

        private void UpdateUI()
        {
            EnemyLevelData enemyCurrentLevelData = _enemyTypeSO.GetEnemyLevelData(_levelable.Level);
            _enemyImage.sprite = enemyCurrentLevelData.AppearanceSpriteFront;
            if (_levelable.IsMaximumLevel)
            {
                _backgroundImage.color = _unlockedColor;
                _experienceImage.color = _unlockedColor;
            }
            UpdateExperienceUI();
        }

        private void UpdateExperienceUI()
        {
            _experienceImage.fillAmount = _levelable.CurrentExperience / _levelable.RequiredExperience;
        }
    }
}
