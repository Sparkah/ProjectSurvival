using _ProjectSurvival.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Audio
{
    public class AudioSwitch : MonoBehaviour
    {
        [Inject] private World _world;
        [SerializeField] private AudioSource[] _audioSources;
        private CompositeDisposable _disposables = new CompositeDisposable();

        private void Start()
        {
            ChangeSoundStatus(_world.IsSoundOn.Value);
            _world.IsSoundOn.Subscribe(x => ChangeSoundStatus(x)).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        private void ChangeSoundStatus(bool isOn)
        {
            for (int i = 0; i < _audioSources.Length; i++)
                _audioSources[i].mute = !isOn;
        }
    }
}
