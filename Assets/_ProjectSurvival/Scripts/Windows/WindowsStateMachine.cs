using UnityEngine;

namespace _ProjectSurvival.Scripts.Windows
{
    /// <summary>
    /// State machine that controls behaviour of selected windows.
    /// </summary>
    public class WindowsStateMachine : MonoBehaviour
    {
        [SerializeField] private Window _defaultWindow;
        [SerializeField] private Window[] _windows;
        private Window _activeWindow;

        private void Start()
        {
            //TODO: Silent close all windows on Start (without event)
            OpenDefault();
            StartListeningOpenEvent();
        }

        private void OnDestroy()
        {
            StopListeningOpenEvent();
            StopListeningActiveCloseEvent();
        }

        private void StartListeningOpenEvent()
        {
            for (int i = 0; i < _windows.Length; i++)
                _windows[i].OnOpen += OnWindowOpened;
        }

        private void StopListeningOpenEvent()
        {
            for (int i = 0; i < _windows.Length; i++)
                _windows[i].OnOpen -= OnWindowOpened;
        }

        /// <summary>
        /// Change active window when another was opened.
        /// </summary>
        /// <param name="openedWindow">Opened window.</param>
        private void OnWindowOpened(Window openedWindow)
        {
            if (openedWindow != _defaultWindow)
            {
                StopListeningActiveCloseEvent();
                _activeWindow.Close();
                ChangeActiveWindow(openedWindow);
            }
        }

        /// <summary>
        /// Open default window if active was closed.
        /// </summary>
        /// <param name="closedWindow">Closed window.</param>
        private void OnActiveClosed(Window closedWindow)
        {
            StopListeningActiveCloseEvent();
            OpenDefault();
        }

        private void StopListeningActiveCloseEvent()
        {
            if (_activeWindow)
                _activeWindow.OnClose -= OnActiveClosed;
        }

        private void ChangeActiveWindow(Window newActive)
        {
            _activeWindow = newActive;
            _activeWindow.OnClose += OnActiveClosed;
        }

        /// <summary>
        /// Change active window to default.
        /// </summary>
        private void OpenDefault()
        {
            if (_defaultWindow)
            {
                _defaultWindow.Open();
                ChangeActiveWindow(_defaultWindow);
            }
        }
    }
}
