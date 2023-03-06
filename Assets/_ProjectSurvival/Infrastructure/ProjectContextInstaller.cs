using _ProjectSurvival.Scripts.Audio;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Infrastructure
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private AudioSystem _audioSystem;
        [SerializeField] private GameProgressHandler _gameProgressHandler;
       

        public override void InstallBindings()
        {
            BindGameProgress();
            BindAudioSystem();
        }

        private void BindGameProgress()
        {
            World world = _gameProgressHandler.InitWorld();
            Container.Bind<World>().FromInstance(world).AsSingle();
        }
        

        private void BindAudioSystem()
        {
            Container.Bind<AudioSystem>().FromInstance(_audioSystem).AsSingle();
        }
    }
}