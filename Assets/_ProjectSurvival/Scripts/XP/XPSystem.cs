using System;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSurvival.Scripts.XP
{
    public class XPSystem : MonoBehaviour
    {
        [SerializeField] private Image _xpBar;
        [SerializeField] private Behaviour _levelableObject;
        
        private ILevelable _levelable => _levelableObject as ILevelable;

        private void Awake()
        {
          //  _levelable.OnExperienceChanged += AddXp;
        }

        private void OnDestroy()
        {
         //   _levelable.OnExperienceChanged -= AddXp;
        }

        public void AddXp(float _xpAmount)
        {
            _levelable.AddExperience(_xpAmount);
        }
    }
}
