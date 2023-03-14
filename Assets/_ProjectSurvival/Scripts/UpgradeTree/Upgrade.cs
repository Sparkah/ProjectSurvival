using _ProjectSurvival.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class Upgrade : MonoBehaviour
    {
        public Image UpgradeImage;
        public Button Button;
        public TextMeshProUGUI Text;
        
        private int _price;
        private UpgradeTypes _upgradeType;

        [Inject] private World _world;

        public void Construct(Sprite upgradeSprite, int price, UpgradeTypes upgradeType)
        {
            _upgradeType = upgradeType;
            UpgradeImage.sprite = upgradeSprite;
            _price = price;
            Text.text = _price.ToString();
        }

        private void Start()
        {
            Button.onClick.AddListener(TryPurchaseUpgrade);
        }

        private void TryPurchaseUpgrade()
        {
            if (_world.Gold.Value >= _price)
            {
                Debug.Log($"Upgrade {_upgradeType} purchased! It's level is {_world.UpgradesLevels[_upgradeType]}");
                _world.Gold.Value -= _price;
                _world.UpgradesLevels[_upgradeType] += 1;
            }
            else
            {
                Debug.Log($"Not enough money! You currently have {_world.Gold.Value} coins, the upgrade cost is {_price} coins");
            }
        }
    }
}