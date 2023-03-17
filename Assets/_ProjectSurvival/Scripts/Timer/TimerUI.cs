using UnityEngine;

namespace _ProjectSurvival.Scripts.Timer
{
    public abstract class TimerUI : MonoBehaviour
    {
        [SerializeField] private Behaviour _timerObject;
        private ITimer _timer => _timerObject as ITimer;

        /// <summary>
        /// Update timer UI with new timer value.
        /// </summary>
        /// <param name="time">Updated time value.</param>
        protected abstract void UpdateUI(float time);

        private void OnEnable()
        {
            _timer.OnTimeUpdate += OnTimerUpdate;
        }

        private void OnDisable()
        {
            _timer.OnTimeUpdate -= OnTimerUpdate;
        }

        private void OnTimerUpdate()
        {
            UpdateUI(_timer.CurrentTime);
        }
    }
}
