using _ProjectSurvival.Scripts.Enemies.Types;
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
        [SerializeField] private Color _lockedColor;
        [SerializeField] private Sprite _lockedEnemySprite;
        [SerializeField] private Sprite _unlockedProgressSprite;
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
            Recolor(_lockedColor);
        }

        public void Init(EnemyTypeSO enemyType, ILevelable levelable)
        {
            _enemyTypeSO = enemyType;
            _levelable = levelable;
            Recolor(_enemyTypeSO.TypeColor);
            UpdateUI();
            _levelable.OnExperienceChanged += UpdateExperienceUI;
            _levelable.OnLevelUp += UpdateUI;
        }

        private void Recolor(Color color) 
        {
            _backgroundImage.color = color;
            _experienceImage.color = color;
        }

        private void UpdateUI()
        {
            EnemyLevelData enemyCurrentLevelData = _enemyTypeSO.GetEnemyLevelData(_levelable.Level);
            _enemyImage.sprite = enemyCurrentLevelData.AppearanceSpriteFront;
            if (_levelable.IsMaximumLevel)
            {
                _experienceImage.sprite = _unlockedProgressSprite;
                _experienceImage.fillAmount = 1;
            }
            UpdateExperienceUI();
        }

        private void UpdateExperienceUI()
        {
            if (_levelable.RequiredExperience != 0)
                _experienceImage.fillAmount = _levelable.CurrentExperience / _levelable.RequiredExperience;
        }

    }
}
