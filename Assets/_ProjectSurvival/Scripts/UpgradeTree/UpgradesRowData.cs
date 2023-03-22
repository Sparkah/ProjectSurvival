using System;
using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradesRowData : MonoBehaviour
    {
        [SerializeField] private UpgradeTypes _upgradeType;
        [SerializeField] private int[] _costProgression = new[] {5}; //Change to read this from SO for refactoring
        private string _upgradeDescription;
        private string _upgradeName;
        [SerializeField] private ScriptableObject _upgradeScriptbaleObject;

        [Inject] private World _world;
        private UpgradeTree _upgradeTree;
        private int _currentUpgrade;
        private Upgrade[] _upgrades = new Upgrade[] {};
        private UpgradePopup _upgradePopup;
        private Sprite _upgradeImage;

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
            if (_world.UpgradeLevels[_upgradeType] < _currentUpgrade)
            {
                upgrade.Button.interactable = false;
                upgrade.Construct(_upgradeImage, _costProgression[_currentUpgrade], _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType, _upgradeScriptbaleObject, _currentUpgrade, true);
            }
            if (_world.UpgradeLevels[_upgradeType] > _currentUpgrade)
            {
                upgrade.Button.interactable = false;
                upgrade.Construct(_upgradeImage, _costProgression[_currentUpgrade], _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType, _upgradeScriptbaleObject, _currentUpgrade, false);
            }
            if (_world.UpgradeLevels[_upgradeType] == _currentUpgrade)
            {
                upgrade.Construct(_upgradeImage, _costProgression[_currentUpgrade], _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType, _upgradeScriptbaleObject, _currentUpgrade, false);
                upgrade.Button.enabled = true;
                upgrade.Button.interactable = true;
            }
            _currentUpgrade += 1;
        }

        private async void DisableButton(Button button)
        {
            ColorBlock colorBlock = button.GetComponent<Button>().colors;
            colorBlock.disabledColor = new Color(195, 255, 104, 1f);
            button.GetComponent<Button>().colors = colorBlock;
            await UniTask.DelayFrame(50);
            button.interactable = false;
        }
    }
}