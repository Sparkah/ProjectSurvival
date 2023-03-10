using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Experience
{
    public class ExperiencePoolInstaller : MonoInstaller
    {
        [SerializeField] private ExperiencePool _experiencePool;
        public override void InstallBindings()
        {
            Container.Bind<ExperiencePool>().FromInstance(_experiencePool).AsSingle();
        }
    }
}
