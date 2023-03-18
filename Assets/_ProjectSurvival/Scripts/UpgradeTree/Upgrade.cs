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
        private string _upgradeType;
        private string _upgradeDescription;

        public void Construct(Sprite upgradeSprite, int price, string upgradeType, UpgradePopup upgradePopup, string upgradeDescription)
        {
            _upgradeType = upgradeType;
            UpgradeImage.sprite = upgradeSprite;
            _price = price;
            Text.text = _price.ToString();
            _upgradePopup = upgradePopup;
            _upgradeDescription = upgradeDescription;
        }

        private void Start()
        {
            Button.onClick.AddListener(OpenUpgradePopup);
        }
 
        private void OpenUpgradePopup()
        {
            _upgradePopup.gameObject.SetActive(true);
            _upgradePopup.SetUp(_price.ToString(), _upgradeType, _upgradeDescription, _price);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}