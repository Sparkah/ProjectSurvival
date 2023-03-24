using _ProjectSurvival.Scripts.Timers;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow
{
    public class SessionTimerInstaller : MonoInstaller
    {
        [SerializeField] private Timer _timer;
        public override void InstallBindings()
        {
            Container.Bind<Timer>().WithId("SessionTimer").FromInstance(_timer).AsSingle();
        }
    }
}
