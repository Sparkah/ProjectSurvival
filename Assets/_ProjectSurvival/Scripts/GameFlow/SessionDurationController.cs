using _ProjectSurvival.Scripts.GameFlow.SessionOver;
using _ProjectSurvival.Scripts.Timers;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow
{
    public class SessionDurationController : MonoBehaviour
    {
        [Header("Time in seconds")]
        [SerializeField] private int _targetTime;
        [Inject(Id = "SessionTimer")] Timer _timer;
        [Inject] private SessionOverController _sessionOverController;

        private void Start()
        {
            _timer.OnTimeUpdate += CheckGameComplete;
        }

        private void OnDestroy()
        {
            _timer.OnTimeUpdate -= CheckGameComplete;
        }

        private void CheckGameComplete()
        {
            if (_timer.CurrentTime >= _targetTime)
            {
                _timer.OnTimeUpdate -= CheckGameComplete;
                _sessionOverController.EndSession(SessionResult.Survived10Minutes);
            }
        }
    }
}
