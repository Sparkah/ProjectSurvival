using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Tutorial
{
    public class TutorialWindowManager : MonoBehaviour //solid плачет по этой системе (((((
    {
        private TutorialWindow[] _tutorialWindows;

        private int _currentWindow = 0;
        public event UnityAction<int> OnWindowChanged;

        private void Awake()
        {
            _tutorialWindows = GetComponentsInChildren<TutorialWindow>();
            SetUpTutorialWindows();
        }

        private void SetUpTutorialWindows()
        {
            for (int i = 0; i < _tutorialWindows.Length; i++)
            {
                if (i == 0)
                {
                    _tutorialWindows[i].gameObject.SetActive(true);
                    _tutorialWindows[i].Set(true,false);
                }
                else
                {
                    _tutorialWindows[i].gameObject.SetActive(false);
                }

                if (i == _tutorialWindows.Length - 1)
                {
                    _tutorialWindows[i].Set(false, true);
                }
            }
        }

        public void NextWindow()
        {
            _tutorialWindows[_currentWindow].gameObject.SetActive(false);
            _currentWindow += 1;
            _tutorialWindows[_currentWindow].gameObject.SetActive(true);
            OnWindowChanged?.Invoke(_currentWindow);
        }

        public void PreviousWindow()
        {
            _tutorialWindows[_currentWindow].gameObject.SetActive(false);
            _currentWindow -= 1;
            _tutorialWindows[_currentWindow].gameObject.SetActive(true);
            OnWindowChanged?.Invoke(_currentWindow);
        }

        public int ReturnWindowCount()
        {
            return _tutorialWindows.Length;
        }

        public int ReturnCurrentPage()
        {
            return _currentWindow;
        }
    }
}