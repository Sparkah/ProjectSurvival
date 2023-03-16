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
        
        void Awake()
        {
            _upgradeTree = GetComponentInParent<UpgradeTree>();
            _upgrades = GetComponentsInChildren<Upgrade>();
            _upgradePopup = _upgradeTree.UpgradePopup;
            foreach (var upgrade in _upgrades)
            {
                SetUpButton(upgrade);
            }
        }

        private void SetUpButton(Upgrade upgrade)
        {
            upgrade.Construct(_upgradeImage, _costProgression[_currentUpgrade], _upgradeName, _upgradePopup, _upgradeDescription);

            if (_world.UpgradeLevels[_upgradeType] < _currentUpgrade)
            {
                upgrade.Button.interactable = false;
            }
            _currentUpgrade += 1;
        }
    }
}