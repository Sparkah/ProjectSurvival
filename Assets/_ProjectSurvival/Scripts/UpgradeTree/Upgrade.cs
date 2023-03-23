using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class Upgrade : MonoBehaviour
    {
        public Image UpgradeImage;
        public Button Button;
        public TextMeshProUGUI Text;
        public Image _takenImage;
        private UpgradePopup _upgradePopup;
        private int _price;
        private string _upgradeName;
        private string _upgradeDescription;
        private UpgradeTypes _upgradeType;
        private ScriptableObject _upgradeScriptableObject;
        private int _id = 0;

        public void Construct(Sprite upgradeSprite, string upgradeName, UpgradePopup upgradePopup, string upgradeDescription, UpgradeTypes upgradeType, ScriptableObject upgradeScriptableObject, int currentUpgrade, bool taken)
        {
            _upgradeName = upgradeName;
            UpgradeImage.sprite = upgradeSprite;
            _upgradePopup = upgradePopup;
            _upgradeDescription = upgradeDescription;
            _upgradeType = upgradeType;
            _upgradeScriptableObject = upgradeScriptableObject;
            _id = currentUpgrade;
            DrawScriptableObjectUpgradeData();
            if (taken)
            {
                _takenImage.enabled = true;
            }
        }

        private void Start()
        {
            Button.onClick.AddListener(OpenUpgradePopup);
        }

        private void DrawScriptableObjectUpgradeData()
        {
            if (_upgradeScriptableObject.GetType() == typeof(WeaponTypeSO))
            {
                var upgradeScriptableObject = _upgradeScriptableObject as WeaponTypeSO;
                if (_id >= upgradeScriptableObject.MaximumLevel)
                {
                    gameObject.SetActive(false);
                    return;
                }

                Text.text = upgradeScriptableObject.GetLevelUpDescription(_id+1);
                UpgradeImage.sprite = upgradeScriptableObject.Picture;
                _upgradeName = upgradeScriptableObject.Title;
                _upgradeDescription = upgradeScriptableObject.Description;
                _price = upgradeScriptableObject.CostProgression[_id];
            }

            if (_upgradeScriptableObject.GetType() == typeof(StatsTypeSO))
            {
                var upgradeScriptableObject = _upgradeScriptableObject as StatsTypeSO;
                if (_id >= upgradeScriptableObject.StatsIncrease.Length)
                {
                    gameObject.SetActive(false);
                    return;
                }
                Text.text = upgradeScriptableObject.StatsIncrease[_id].ToString()+"%";
                UpgradeImage.sprite = upgradeScriptableObject.Picture;
                _upgradeName = upgradeScriptableObject.Title;
                _upgradeDescription = upgradeScriptableObject.Description;
                _price = upgradeScriptableObject.CostProgression[_id];
            }
        }

        private void OpenUpgradePopup()
        {
            _upgradePopup.gameObject.SetActive(true);
            _upgradePopup.SetUp(_upgradeName, _upgradeDescription, _price, _upgradeType, Text.text);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}