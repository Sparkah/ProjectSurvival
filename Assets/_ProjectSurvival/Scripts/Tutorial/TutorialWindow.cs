using System;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.Tutorial
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        private int _currentPage;

        public void Set(bool nextBtn, bool prevButton)
        {
            _nextButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);
            if (!nextBtn)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (!prevButton)
            {
                _previousButton.gameObject.SetActive(false);
            }
        }
    }
}
