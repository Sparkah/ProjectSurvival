using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.GameFlow.Statistics
{
    public class EnemyLevelResultUI : MonoBehaviour
    {
        [SerializeField] private Text _defeatedCountText;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _totalCurrencyText;

        public void FillData(int defeatedCount, float price, float totalCurrency)
        {
            _defeatedCountText.text = defeatedCount.ToString();
            _priceText.text = price.ToString();
            _totalCurrencyText.text = Mathf.FloorToInt(totalCurrency).ToString();
        }
    }
}
