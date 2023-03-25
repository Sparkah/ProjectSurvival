using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.GameFlow.Statistics
{
    public class EnemyLevelResultUI : MonoBehaviour
    {
        [Header("Text UI")]
        [SerializeField] private Text _defeatedCountText;
        [SerializeField] private string _priceLockedText = "???";
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _totalCurrencyText;
        [Header("Picture")]
        [SerializeField] private Image _enemiesImage;
        [SerializeField] private Sprite _lockedPicture;
        [SerializeField] private Sprite _unlockedPicture;

        public void FillData(int defeatedCount, float price, float totalCurrency)
        {
            _defeatedCountText.text = defeatedCount.ToString();
            _enemiesImage.sprite = defeatedCount > 0 ? _unlockedPicture : _lockedPicture;
            _priceText.text = defeatedCount > 0 ? price.ToString() : _priceLockedText;
            _totalCurrencyText.text = Mathf.FloorToInt(totalCurrency).ToString();
        }
    }
}
