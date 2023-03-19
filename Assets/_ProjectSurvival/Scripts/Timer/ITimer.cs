using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Timer
{
    /// <summary>
    /// Object that updates current time if it is counting.
    /// </summary>
    public interface ITimer
    {
        public float CurrentTime { get; }
        public bool IsCounting { get; }

        public event UnityAction OnTimeUpdate;
    }
}
