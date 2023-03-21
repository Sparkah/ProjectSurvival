using _ProjectSurvival.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectSurvival.Scripts.Audio
{
    public class AudioSwitcherUI : MonoBehaviour
    {
        [Inject] private World _world;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;
        private CompositeDisposable _disposables = new CompositeDisposable();

        private void Start()
        {
            ChangeUI(_world.IsSoundOn.Value);
            _world.IsSoundOn.Subscribe(x => ChangeUI(x)).AddTo(_disposables);
            _button.onClick.AddListener(ChangeSoundStatus);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
            _button.onClick.RemoveListener(ChangeSoundStatus);
        }

        public void ChangeSoundStatus()
        {
            _world.IsSoundOn.Value = !_world.IsSoundOn.Value;
        }

        private void ChangeUI(bool isOn)
        {
            _image.sprite = isOn ? _onSprite : _offSprite;
        }
    }
}
