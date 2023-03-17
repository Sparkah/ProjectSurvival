using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Timer
{
    /// <summary>
    /// Timer realization via deltaTime.
    /// </summary>
    public class Timer : MonoBehaviour, ITimer
    {
        [SerializeField] private bool _runOnAwake;
        private bool _isCounting;
        private float _currentTime;

        public float CurrentTime => _currentTime;
        public bool IsCounting => _isCounting;

        public event UnityAction OnTimeUpdate;

        private void Awake()
        {
            if (_runOnAwake)
                _isCounting = true;
        }

        private void Update()
        {
            if (_isCounting)
            {
                _currentTime += Time.deltaTime;
                OnTimeUpdate?.Invoke();
            }
        }
    }
}
