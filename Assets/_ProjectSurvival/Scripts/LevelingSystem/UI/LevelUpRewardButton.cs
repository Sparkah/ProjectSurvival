using _ProjectSurvival.Scripts.LevelingSystem.Rewards;
using System.Collections.Generic;
using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.LevelingSystem.UI
{
    public class LevelUpRewardButton : MonoBehaviour
    {
        [Inject] private AudioSystem _audioSystem;
        
        [SerializeField] private Button _button;
        [SerializeField] private Text _rewardTitleLabel;
        [SerializeField] private Text _rewardDescriptionLabel;
        [SerializeField] private Image _rewardPicture;
        private LevelUpWindow _levelUpWindow;
        private List<IRewardGiver> _rewardGiver = new List<IRewardGiver>();
        private IReward _reward;
        private RewardType _rewardType;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnCick);
        }

        public void Init(LevelUpWindow levelUpWindow, List<IRewardGiver> rewardGiver = null)
        {
            _levelUpWindow = levelUpWindow;
            _rewardGiver = rewardGiver;
            _button.onClick.AddListener(OnCick);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowReward(IReward reward)
        {
            _reward = reward;
            _rewardType = _reward.GetRewardType();
            _rewardTitleLabel.text = _reward.Title;
            _rewardDescriptionLabel.text = _reward.Description;
            _rewardPicture.sprite = _reward.Picture;
            gameObject.SetActive(true);
        }

        private void OnCick()
        {
            if (_reward != null && _rewardGiver != null)
                foreach (var rewardGiver in _rewardGiver)
                {
                    rewardGiver.GiveReward(_reward);
                }
            // _rewardGiver.GiveReward(_reward);
            _audioSystem.StopSceneMusic(false);
            _levelUpWindow.CloseLevelUpWindow();
        }
    }
}
