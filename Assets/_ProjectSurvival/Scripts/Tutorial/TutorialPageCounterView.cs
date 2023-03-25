using System;
using TMPro;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Tutorial
{
    public class TutorialPageCounterView : MonoBehaviour
    {
        private TutorialWindowManager _tutorialWindowManager;
        private TextMeshProUGUI _text;

        void Start()
        {
            _tutorialWindowManager = GetComponentInParent<TutorialWindowManager>();
            _text = GetComponent<TextMeshProUGUI>();
            _tutorialWindowManager.OnWindowChanged += ChangeView;
            ChangeView(_tutorialWindowManager.ReturnCurrentPage());
        }

        private void OnDestroy()
        {
            _tutorialWindowManager.OnWindowChanged -= ChangeView;
        }

        private void ChangeView(int window)
        {
            _text.text = (window+1).ToString() + "/" + _tutorialWindowManager.ReturnWindowCount();
        }
    }
}
