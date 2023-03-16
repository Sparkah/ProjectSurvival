using _ProjectSurvival.Infrastructure;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradesRowData : MonoBehaviour
    {
        [SerializeField] private UpgradeTypes _upgradeType;
        [SerializeField] private int[] _costProgression = new[] {5};
        [SerializeField] private Sprite _upgradeImage;

        [Inject] private World _world;
        private int _currentUpgrade;
        private Upgrade[] _upgrades = new Upgrade[] {};
        
        void Awake()
        {
            _upgrades = GetComponentsInChildren<Upgrade>();
            foreach (var upgrade in _upgrades)
            {
                SetUpButton(upgrade);
            }
        }

        private void SetUpButton(Upgrade upgrade)
        {
           // upgrade.UpgradeImage.sprite = _upgradeImage;
            //upgrade.Price = _costProgression[_currentUpgrade];
            //upgrade.UpgradeType = _upgradeType;
            
            upgrade.Construct(_upgradeImage, _costProgression[_currentUpgrade], _upgradeType);

            if (_world.UpgradeLevels[_upgradeType] < _currentUpgrade)
            {
                upgrade.Button.interactable = false;
            }
            _currentUpgrade += 1;
        }
    }
}