using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.UpgradeTree
{
    public class Upgrade : MonoBehaviour
    {
        [Inject] private World _world;
        
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
        private CostProgressionSO _costProgression;
        private RewardType _upgradeRewardType;

        public void Construct(Sprite upgradeSprite, string upgradeName, UpgradePopup upgradePopup, string upgradeDescription, UpgradeTypes upgradeType, ScriptableObject upgradeScriptableObject, int currentUpgrade, bool taken, CostProgressionSO costProgression)
        {
            _costProgression = costProgression;
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

                _upgradeRewardType = RewardType.Weapon;
                Text.text = upgradeScriptableObject.GetLevelUpDescription(_id+1);
                Text.enabled = false;
                UpgradeImage.sprite = upgradeScriptableObject.Picture;
                _upgradeName = upgradeScriptableObject.Title;
                _upgradeDescription = upgradeScriptableObject.Description;
                _price = _costProgression.CostProgression[_world.CurrentUpgradeID.Value];
            }

            if (_upgradeScriptableObject.GetType() == typeof(StatsTypeSO))
            {
                var upgradeScriptableObject = _upgradeScriptableObject as StatsTypeSO;
                if (_id >= upgradeScriptableObject.StatsIncrease.Length)
                {
                    gameObject.SetActive(false);
                    return;
                }
                
                _upgradeRewardType = RewardType.Stat;
                Text.text = upgradeScriptableObject.StatsIncrease[_id].ToString()+"%";
                UpgradeImage.sprite = upgradeScriptableObject.Picture;
                _upgradeName = upgradeScriptableObject.Title;
                //_upgradeDescription = upgradeScriptableObject.Description;
                Debug.Log(_id);
                _upgradeDescription = upgradeScriptableObject.GetLevelUpDescription(_id+1);
                _price = _costProgression.CostProgression[_world.CurrentUpgradeID.Value];
            }
        }

        private void OpenUpgradePopup()
        {
            _upgradePopup.gameObject.SetActive(true);
            _upgradePopup.SetUp(_upgradeName, _upgradeDescription, _price, _upgradeType, Text.text, _upgradeRewardType);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}