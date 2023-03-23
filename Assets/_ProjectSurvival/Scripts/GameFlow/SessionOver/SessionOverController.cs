using UnityEngine;

namespace _ProjectSurvival.Scripts.GameFlow.SessionOver
{
    public class SessionOverController : MonoBehaviour
    {
        public event System.Action<SessionResult> OnSessionEnded;
        public void EndSession(SessionResult sessionResult)
        {
            OnSessionEnded?.Invoke(sessionResult);
        }
    }
}
