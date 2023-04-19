using _ProjectSurvival.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _ProjectSurvival.Scripts.Tutorial.HighLightedTutorials
{
    public class PlayerHelper : MonoBehaviour
    {
        //[Inject] private World _world;
        //private CompositeDisposable _disposables = new CompositeDisposable();
        
        [SerializeField] private TutorialPlayGame _tutorialPlayGame;
        [SerializeField] private TutorialSeeTutorial _tutorialSeeTutorial;
        [SerializeField] private TutorialUpgradeTree _tutorialUpgradeTree;

        public event UnityAction OnUpdateTutorialStates;

        private void Awake()
        {
            _tutorialPlayGame.Construct(this);
            _tutorialSeeTutorial.Construct(this);
            _tutorialUpgradeTree.Construct(this);
            //_world.Gold.Subscribe(x => UpdateStates()).AddTo(_disposables);
        }

        public void UpdateStates()
        {
            OnUpdateTutorialStates?.Invoke();
        }
        
        private void OnDestroy()
        {
            //_disposables.Clear();
        }
    }
}
