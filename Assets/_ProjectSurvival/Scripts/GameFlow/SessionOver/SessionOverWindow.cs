using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    public class SessionOverWindow : MonoBehaviour
    {
        [SerializeField] private PauseControllerSO _pauseControllerSO;
        [SerializeField] private Window _window;
        private SessionOverController _sessionOverController;

        [Inject]
        private void Construct(SessionOverController sessionOverController)
        {
            _sessionOverController = sessionOverController;
            _sessionOverController.OnSessionEnded += ShowWindow;
        }

        private void OnDestroy()
        {
            _sessionOverController.OnSessionEnded -= ShowWindow;
        }

        private void ShowWindow(SessionResult sessionResult)
        {
            _pauseControllerSO.PauseGame();
            _window.Open();
        }
    }
}
