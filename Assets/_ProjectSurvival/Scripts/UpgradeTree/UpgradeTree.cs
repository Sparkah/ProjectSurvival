using System.Linq;
using _ProjectSurvival.Infrastructure;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradeTree : MonoBehaviour
    {
        [Inject] private World _world;
        
        public UpgradePopup UpgradePopup;
        
        [SerializeField] private TextMeshProUGUI _upgradesAvailable;
        [SerializeField] private TextMeshProUGUI _upgradesMastered;
        
        private UpgradesRowData[] _upgradesRowDatas;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private ReactiveProperty<int> _purchasedUpgrades = new ReactiveProperty<int>();
        
        private void Start()
        {
            UpgradePopup.Construct(this);
            _upgradesRowDatas = GetComponentsInChildren<UpgradesRowData>();
            _world.UpgradeLevels.ObserveReplace().Subscribe(x => _purchasedUpgrades.Value+=1).AddTo(_disposable);
            _purchasedUpgrades.Subscribe(_ => UpdateUpgradesCount()).AddTo(_disposable);
            SetUpUpgradeRows();
            UpdateUpgradesCount();
        }

        private void UpdateUpgradesCount()
        {
            var amountTotal = _upgradesRowDatas.Sum(upgradeRaw => upgradeRaw.GetUpgradesTotalAmount());
            var amountMastered = 0;
            foreach (var worldUpgradeLevel in _world.UpgradeLevels)
            {
                if (worldUpgradeLevel.Value > 0)
                {
                    amountMastered += worldUpgradeLevel.Value;
                }
            }

            _upgradesAvailable.text = "Доступно: " + amountTotal;
            _upgradesMastered.text = "Улучшено: " + (amountMastered).ToString();
        }

        public void SetUpUpgradeRows()
        {
            foreach (var upgradesRowData in _upgradesRowDatas)
            {
                upgradesRowData.SetUp();
            }
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}