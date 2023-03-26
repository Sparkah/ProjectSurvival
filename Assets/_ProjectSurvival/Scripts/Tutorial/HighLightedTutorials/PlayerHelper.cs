using System;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Tutorial.HighLightedTutorials
{
    public class PlayerHelper : MonoBehaviour
    {
        [SerializeField] private TutorialPlayGame _tutorialPlayGame;
        [SerializeField] private TutorialSeeTutorial _tutorialSeeTutorial;
        [SerializeField] private TutorialUpgradeTree _tutorialUpgradeTree;

        public event UnityAction OnUpdateTutorialStates;

        private void Awake()
        {
            _tutorialPlayGame.Construct(this);
            _tutorialSeeTutorial.Construct(this);
            _tutorialUpgradeTree.Construct(this);
        }

        public void UpdateStates()
        {
            OnUpdateTutorialStates?.Invoke();
        }
    }
}
