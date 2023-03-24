using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.GameFlow.Statistics
{
    public class StatisticValueUI : MonoBehaviour
    {
        [SerializeField] private Text _valueText;

        public void FillData(float value)
        {
            _valueText.text = Mathf.FloorToInt(value).ToString();
        }
    }
}
