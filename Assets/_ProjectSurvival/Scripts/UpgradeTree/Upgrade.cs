using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class Upgrade : MonoBehaviour
    {
        public Image UpgradeImage;
        public Button Button;
        public TextMeshProUGUI Text;
        
        private UpgradePopup _upgradePopup;
        private int _price;
        private string _upgradeName;
        private string _upgradeDescription;
        private UpgradeTypes _upgradeType;

        public void Construct(Sprite upgradeSprite, int price, string upgradeName, UpgradePopup upgradePopup, string upgradeDescription, UpgradeTypes upgradeType)
        {
            _upgradeName = upgradeName;
            UpgradeImage.sprite = upgradeSprite;
            _price = price;
            Text.text = _price.ToString();
            _upgradePopup = upgradePopup;
            _upgradeDescription = upgradeDescription;
            _upgradeType = upgradeType;
        }

        private void Start()
        {
            Button.onClick.AddListener(OpenUpgradePopup);
        }
 
        private void OpenUpgradePopup()
        {
            _upgradePopup.gameObject.SetActive(true);
            _upgradePopup.SetUp(_price.ToString(), _upgradeName, _upgradeDescription, _price, _upgradeType);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}