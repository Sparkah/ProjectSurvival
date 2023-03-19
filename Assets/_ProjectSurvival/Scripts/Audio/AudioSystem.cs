using _ProjectSurvival.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ProjectSurvival.Scripts.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        [Header("Main sound themes")]
        [SerializeField] private AudioClip _menuSceneMusic;
        [SerializeField] private AudioClip _levelSceneMusic;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Equals(SceneNames.MenuScene))
                _audioSource.clip = _menuSceneMusic;
            else
            {
                _audioSource.clip = _levelSceneMusic;
            }

            _audioSource.Play();
        }

        public void StopSceneMusic(bool stop)
        {
            if (stop)
            {
                _audioSource.Pause();
            }

            if (!stop)
            {
                _audioSource.Play();
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}