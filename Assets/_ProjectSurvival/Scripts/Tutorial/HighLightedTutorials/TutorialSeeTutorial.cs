using _ProjectSurvival.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.Tutorial.HighLightedTutorials
{
    public class TutorialSeeTutorial : MonoBehaviour
    {
        [Inject] private World _world;
        [SerializeField] private TweenableButton _tweenableButton;
        [SerializeField] private Button[] _buttonToCompleteTutorial;
        private PlayerHelper _playerHelper;

        public void Construct(PlayerHelper playerHelper)
        {
            _playerHelper = playerHelper;
            _playerHelper.OnUpdateTutorialStates += TryWatchTutorial;
        }
        
        private void Start()
        {
            TryWatchTutorial();
        }

        private void TryWatchTutorial()
        {
            _world.Tutorial.TryGetValue(TutorialEnum.WatchTutorial, out bool val);
            if (val == false)
            {
                foreach (var button in _buttonToCompleteTutorial)
                {
                    button.onClick.AddListener(CompleteTutorial);
                }
                _tweenableButton.Tween();
            }
            else
            {
                _tweenableButton.StopTween();
            }
        }

        private void OnDestroy()
        {
            foreach (var button in _buttonToCompleteTutorial)
            {
                button.onClick.RemoveAllListeners();
            }
            _tweenableButton.StopTween();
            _playerHelper.OnUpdateTutorialStates -= TryWatchTutorial;
        }

        private void CompleteTutorial()
        {
            _world.Tutorial.Add(TutorialEnum.WatchTutorial, true);
            _playerHelper.UpdateStates();
        }
    }
}
