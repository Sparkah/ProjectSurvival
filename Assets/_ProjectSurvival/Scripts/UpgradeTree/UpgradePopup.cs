using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _skillStatText;
        [SerializeField] private TextMeshProUGUI _skillNameText;
        [SerializeField] private TextMeshProUGUI _skillDescriptionText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _upgradeButton;

        public void SetUp(string skillStatText, string skillNameText, string skillDescriptionText, string priceText)
        {
            _skillStatText.text = skillStatText;
            _skillNameText.text = skillNameText;
            _skillDescriptionText.text = skillDescriptionText;
            _priceText.text = priceText;
        }
    }
}