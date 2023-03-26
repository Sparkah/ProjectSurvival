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
        }

        private void PlaySound()
        {
            switch (_open)
            {
                case true:
                    AudioPlayer.Audio.PlayOneShotSound(AudioSounds.ConfirmUI);
                    break;
                case false:
                    AudioPlayer.Audio.PlayOneShotSound(AudioSounds.CancelUI);
                    break;
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
