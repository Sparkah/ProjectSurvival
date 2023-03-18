using System;
using _ProjectSurvival.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradePopup : MonoBehaviour
    {
        [Inject] private World _world;
        
        [SerializeField] private TextMeshProUGUI _skillStatText;
        [SerializeField] private TextMeshProUGUI _skillNameText;
        [SerializeField] private TextMeshProUGUI _skillDescriptionText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _upgradeButton;

        private int _price;
        private UpgradeTypes _upgradeType;
        private UpgradeTree _upgradeTree;

        public void Construct(UpgradeTree upgradeTree)
        {
            _upgradeTree = upgradeTree;
        }
        
        public void SetUp(string skillStatText, string skillNameText, string skillDescriptionText, int price, UpgradeTypes upgradeType)
        {
            _skillStatText.text = skillStatText;
            _skillNameText.text = skillNameText;
            _skillDescriptionText.text = skillDescriptionText;
            _price = price;
            _upgradeButton.onClick.AddListener(TryPurchase);
            _priceText.text = _world.Gold.Value+"/" + price.ToString();
            _upgradeType = upgradeType;
        }

        private void TryPurchase()
        {
            //Debug.Log(_price);
            if (_world.Gold.Value >= _price)
            {
                _world.Gold.Value -= _price;
                _world.UpgradeLevels[_upgradeType] += 1;
                gameObject.SetActive(false);
                _upgradeTree.SetUpUpgradeRows();
                Debug.Log($"Upgrade {_upgradeType} purchased! It's level is {_world.UpgradeLevels[_upgradeType]}");
            }
            else
            {
                Debug.Log($"Not enough money! You currently have {_world.Gold.Value} coins, the upgrade cost is {_price} coins");
            }
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveAllListeners();
        }
    }
}