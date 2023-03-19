using _ProjectSurvival.Scripts.Enemies.Evolution;
using _ProjectSurvival.Scripts.LevelingSystem;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Experience
{
    public class ExperienceCollector : MonoBehaviour
    {
        [Inject] private EnemiesEvolutionTracker _enemiesEvolutionTracker;
        [SerializeField] private Behaviour _levelableObject;
        private ILevelable _levelable => _levelableObject as ILevelable;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ExperiencePoint experiencePoint))
            {
                if (experiencePoint.Collect(transform))
                {
                    _levelable.AddExperience(experiencePoint.ExperienceAmount);
                    _enemiesEvolutionTracker.IncreaseEvolutionExperience(experiencePoint.EnemyTypeSO, experiencePoint.ExperienceAmount);
                }
            }
        }
    }
}
