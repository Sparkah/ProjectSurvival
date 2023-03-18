using System;
using UnityEngine;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class UpgradeTree : MonoBehaviour
    {
        public UpgradePopup UpgradePopup;
        private UpgradesRowData[] _upgradesRowDatas;

        private void Start()
        {
            UpgradePopup.Construct(this);
            _upgradesRowDatas = GetComponentsInChildren<UpgradesRowData>();
            SetUpUpgradeRows();
        }

        public void SetUpUpgradeRows()
        {
            foreach (var upgradesRowData in _upgradesRowDatas)
            {
                upgradesRowData.SetUp();
            }
        }
    }
}