using _ProjectSurvival.Scripts.Audio;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Windows
{
    /// <summary>
    /// Base window behaviour.
    /// </summary>
    public abstract class Window : MonoBehaviour
    {
        private bool _isOpened;
        /// <summary>
        /// Is window opened.
        /// </summary>
        public bool IsOpened => _isOpened;

        public event System.Action<Window> OnOpen;
        public event System.Action<Window> OnClose;

        /// <summary>
        /// Custom actions when window is closing.
        /// </summary>
        protected abstract void HandleOpen();
        /// <summary>
        /// Custom actions when window is opening.
        /// </summary>
        protected abstract void HandleClose();

        /// <summary>
        /// Open window.
        /// </summary>
        public void Open()
        {
            HandleOpen();
            _isOpened = true;
            OnOpen?.Invoke(this);
        }

        /// <summary>
        /// Close window.
        /// </summary>
        public void Close()
        {
            HandleClose();
            _isOpened = false;
            OnClose?.Invoke(this);
        }
    }
}
