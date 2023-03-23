using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    public class SessionOverControllerInstaller : MonoInstaller
    {
        [SerializeField] private SessionOverController _sessionOverController;
        public override void InstallBindings()
        {
            Container.Bind<SessionOverController>().FromInstance(_sessionOverController).AsSingle();
        }
    }
}
