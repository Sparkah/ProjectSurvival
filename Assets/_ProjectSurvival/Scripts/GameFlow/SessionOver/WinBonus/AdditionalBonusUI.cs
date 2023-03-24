using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    public class AdditionalBonusUI : MonoBehaviour
    {
        [SerializeField] private AdditionalSessionBonusSO _additionalSessionBonus;
        [Header("UI")]
        [SerializeField] private string _messageTemplate;
        [SerializeField] private Text _winMessage;

        public void HandleAddionalBonus(SessionResult sessionResult, out int colletedBonus)
        {
            colletedBonus = _additionalSessionBonus.CalculateBonus(sessionResult);
            if (colletedBonus > 0)
            {
                _winMessage.text = _messageTemplate.Replace("_", colletedBonus.ToString());
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
