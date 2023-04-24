using _ProjectSurvival.Scripts.Enemies;
using _ProjectSurvival.Scripts.Enemies.Types;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Experience
{
    public class ExperienceHolder : MonoBehaviour
    {
        [Inject] ExperiencePool _experiencePool;
        private float _experienceAmount;

        public void SetupExperience(float amount)
        {
            _experienceAmount = amount;
        }

        public void DropExperiencePoint(EnemyTypeSO enemyTypeSO)
        {
            ExperiencePoint droppingPoint = _experiencePool.Pool.Get();
            droppingPoint.Drop(transform.position, enemyTypeSO, _experienceAmount);
        }
    }
}
