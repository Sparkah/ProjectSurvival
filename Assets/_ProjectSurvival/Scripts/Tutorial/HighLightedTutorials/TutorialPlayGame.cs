using _ProjectSurvival.Infrastructure;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.Tutorial.HighLightedTutorials
{
    public class TutorialPlayGame : MonoBehaviour
    {
        [Inject] private World _world;
        [SerializeField] private TweenableButton _tweenableButton;
        [SerializeField] private Button _buttonToCompleteTutorial;
        private PlayerHelper _playerHelper;

        public void Construct(PlayerHelper playerHelper)
        {
            _playerHelper = playerHelper;
            _playerHelper.OnUpdateTutorialStates += CheckIfGameWasAlreadyPlayed;
        }

        private void Start()
        {
            CheckIfGameWasAlreadyPlayed();
        }

        private void CheckIfGameWasAlreadyPlayed()
        {
            _world.Tutorial.TryGetValue(TutorialEnum.PlayGame, out bool val);
            if (val == false)
            {
                _buttonToCompleteTutorial.onClick.AddListener(CompleteTutorial);
                _world.Tutorial.TryGetValue(TutorialEnum.WatchTutorial, out bool tutorWatched);
                {
                    if (tutorWatched)
                    {
                        _tweenableButton.Tween();
                    }
                    else
                    {
                        _tweenableButton.StopTween();
                    }
                }
            }
            else
            {
                _tweenableButton.StopTween();
            }
        }

        private void OnDestroy()
        {
            _buttonToCompleteTutorial.onClick.RemoveAllListeners();
            _playerHelper.OnUpdateTutorialStates -= CheckIfGameWasAlreadyPlayed;
            _tweenableButton.StopTween();
        }

        private void CompleteTutorial()
        {
            _world.Tutorial.Add(TutorialEnum.PlayGame, true);
            _playerHelper.UpdateStates();
        }
    }
}
