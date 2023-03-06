using UnityEngine;

namespace _ProjectSurvival.Scripts.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Audio { get; private set; }

        [Header("AudioSources")] 
        [SerializeField] private AudioSource _audioSourceMain;
        [SerializeField] private AudioSource _audioSourceSecondary;

        private void Awake()
        {
            if (Audio != null && Audio != this)
                Destroy(this);
            else
                Audio = this;
        }
    }
}
