using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ProjectSurvival.Scripts.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        [Header("Main sound themes")]
        [SerializeField] private AudioClip _menuSceneMusic;
        [SerializeField] private AudioClip _levelSceneMusic;

        [Header("Sounds")] 
        [SerializeField] private AudioClip _levelUp;
        [SerializeField] private AudioClip _chooseWeaponA;
        [SerializeField] private AudioClip _chooseWeaponB;
        
        private AudioSource _audioSource;
        private World _world;

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
                _audioSource.clip = _levelSceneMusic;
        
            _audioSource.Play();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}