using _ProjectSurvival.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradesRowData : MonoBehaviour
    {
        [Inject] private World _world;
        
        [SerializeField] private UpgradeTypes _upgradeType;
        [SerializeField] private ScriptableObject _upgradeScriptbaleObject;
        
        private string _upgradeDescription;
        private string _upgradeName;
        private UpgradeTree _upgradeTree;
        private int _currentUpgrade;
        private Upgrade[] _upgrades = new Upgrade[] {};
        private UpgradePopup _upgradePopup;
        private Sprite _upgradeImage;
        private CostProgressionSO _costProgression;

        private void Awake()
        {
            //_world.Gold.Value += 10;
            _upgradeTree = GetComponentInParent<UpgradeTree>();
            _upgrades = GetComponentsInChildren<Upgrade>();
            _upgradePopup = _upgradeTree.UpgradePopup;
            _costProgression = _upgradeTree.CostProgression;
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
            upgrade.Button.enabled = true;
            upgrade.Button.interactable = true;
            if (_world.UpgradeLevels[_upgradeType] < _currentUpgrade)
            {
                upgrade.Button.interactable = false;
                upgrade.Construct(_upgradeImage, _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType, _upgradeScriptbaleObject, _currentUpgrade, false, _costProgression);
            }
            if (_world.UpgradeLevels[_upgradeType] > _currentUpgrade)
            {
                upgrade.Button.interactable = false;
                upgrade.Construct(_upgradeImage, _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType, _upgradeScriptbaleObject, _currentUpgrade, true, _costProgression);
            }
            if (_world.UpgradeLevels[_upgradeType] == _currentUpgrade)
            {
                upgrade.Construct(_upgradeImage, _upgradeName, _upgradePopup, _upgradeDescription, _upgradeType, _upgradeScriptbaleObject, _currentUpgrade, false, _costProgression);
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