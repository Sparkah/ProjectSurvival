using UnityEngine;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradesRowData : MonoBehaviour
    {
        [SerializeField] private UpgradeTypes _upgradeType;
        [SerializeField] private int[] _costProgression = new[] {5};
        [SerializeField] private Sprite _upgradeImage;

        private Upgrade[] _upgrades = new Upgrade[] {};
        
        void Start()
        {
            _upgrades = GetComponentsInChildren<Upgrade>();
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeImage.sprite = _upgradeImage;
            }
        }
    }
}