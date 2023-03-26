using System;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private bool _open;
        private Button _button;
    
        void Awake()
        {
            _button = GetComponent<Button>();
            //_button.onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            if (_open)
            {
                Debug.Log("confirm");
                AudioPlayer.Audio.PlayOneShotSound(AudioSounds.ConfirmUI);
            }

            if (!_open)
            {
                Debug.Log("cancel");
                AudioPlayer.Audio.PlayOneShotSound(AudioSounds.CancelUI);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PlaySound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
