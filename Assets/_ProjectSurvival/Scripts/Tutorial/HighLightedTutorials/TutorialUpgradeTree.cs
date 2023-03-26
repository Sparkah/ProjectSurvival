using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.UpgradeTree;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Tutorial.HighLightedTutorials
{
    public class TutorialUpgradeTree : MonoBehaviour
    {
        [Inject] private World _world;
        [SerializeField] private TweenableButton _upgradeTreeButton;
        [SerializeField] private CostProgressionSO _costProgressionSO;
        private PlayerHelper _playerHelper;
        
        public void Construct(PlayerHelper playerHelper)
        {
            _playerHelper = playerHelper;
            _playerHelper.OnUpdateTutorialStates += CheckIfPlayerHasEnoughMoney;
        }

        private void Start()
        {
            CheckIfPlayerHasEnoughMoney();
        }

        public void CheckIfPlayerHasEnoughMoney()
        {
            if (_world.Gold.Value >= _costProgressionSO.CostProgression[_world.CurrentUpgradeID.Value])
            {
                _upgradeTreeButton.Tween();
            }
            else
            {
                _upgradeTreeButton.StopTween();
            }
        }

        private void OnDestroy()
        {
            _playerHelper.OnUpdateTutorialStates -= CheckIfPlayerHasEnoughMoney;
        }
    }
}