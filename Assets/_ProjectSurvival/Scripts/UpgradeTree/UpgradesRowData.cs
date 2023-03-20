using System;
using _ProjectSurvival.Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradesRowData : MonoBehaviour
    {
        [SerializeField] private UpgradeTypes _upgradeType;
        [SerializeField] private int[] _costProgression = new[] {5};
        [SerializeField] private Sprite _upgradeImage;
        [SerializeField] private string _upgradeDescription;
        [SerializeField] private string _upgradeName;

        [Inject] private World _world;
        private UpgradeTree _upgradeTree;
        private int _currentUpgrade;
        private Upgrade[] _upgrades = new Upgrade[] {};
        private UpgradePopup _upgradePopup;

        private void Awake()
        {
            _upgradeTree = GetComponentInParent<UpgradeTree>();
            _upgrades = GetComponentsInChildren<Upgrade>();
            _upgradePopup = _upgradeTree.UpgradePopup;
        }

        public int GetUpgradesTotalAmount()
        {
            return _upgrades.Length;
        }

        public void SetUp()
        {
            _currentUpgrade = 0;
            foreach (var upgrade in _upgrades)
            {
                SetUpButton(upgrade);
            }
        }

        private void SetUpButton(Upgrade upgrade)
        {
//            Debug.Log(upgrade.transform.position);
            upgrade.Construct(_upgradeImage, _costProgression[_currentUpgrade], _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType);

            if (_world.UpgradeLevels[_upgradeType] < _currentUpgrade)
            {
                upgrade.Button.interactable = false;
            }
            if (_world.UpgradeLevels[_upgradeType] > _currentUpgrade)
            {
                upgrade.Button.enabled = false;
            }
            if (_world.UpgradeLevels[_upgradeType] == _currentUpgrade)
            {
                upgrade.Button.enabled = true;
                upgrade.Button.interactable = true;
            }
            _currentUpgrade += 1;
        }
    }
}