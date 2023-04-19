using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.Tutorial
{
    [RequireComponent(typeof(Button))]
    public class TweenableButton : MonoBehaviour
    {
        private Button _button;
        private Tween _buttonTween;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnDestroy()
        {
            StopTween();
            if (_button)
                _button.transform.DOKill();
        }

        public void Tween()
        {
            _buttonTween = _button.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f).SetLoops(-1);
        }

        public void StopTween()
        {
            _button.transform.DOKill();
            //_buttonTween.Kill();
        }
    }
}