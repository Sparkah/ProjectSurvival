using _ProjectSurvival.Infrastructure;
using _ProjectSurvival.Scripts.GameFlow.Statistics;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    public class SessionOverWindow : MonoBehaviour
    {
        [SerializeField] private PauseControllerSO _pauseControllerSO;
        [SerializeField] private Window _window;
        [SerializeField] private SessionOverDataUI _sessionOverDataUI;
        [SerializeField] private SessionResultUI _sessionResultUI;
        [SerializeField] private AdditionalBonusUI _additionalBonusUI;
        [SerializeField] private AvailableSessionResultLabelsSO _availableSessionResultLabelsSO;
        private SessionOverController _sessionOverController;
        private World _world;

        [Inject]
        private void Construct(SessionOverController sessionOverController, World world)
        {
            _world = world;
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
            _sessionResultUI.FillData(_availableSessionResultLabelsSO.FindLabelsFor(sessionResult));
            _additionalBonusUI.HandleAddionalBonus(sessionResult, out int collectedBonus);
            _world.Gold.Value += collectedBonus;
            _sessionOverDataUI.UpdateData(collectedBonus);
            _window.Open();
        }
    }
}
