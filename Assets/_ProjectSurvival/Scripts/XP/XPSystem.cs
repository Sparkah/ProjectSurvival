using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.XP
{
    public class XPSystem : MonoBehaviour
    {
        [SerializeField] private Image _xpBar;
        [SerializeField] private Behaviour _levelableObject;
        
        private ILevelable _levelable => _levelableObject as ILevelable;
        public void AddXp(float _xpAmount)
        {
            _levelable.AddExperience(_xpAmount);
        }
    }
}
