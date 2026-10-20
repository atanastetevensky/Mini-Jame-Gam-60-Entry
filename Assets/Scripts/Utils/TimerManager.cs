using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static readonly List<Timer> _timers = new List<Timer>();

    void Update()
    {
        if (_timers.Count == 0) return;

        foreach (Timer timer in _timers.ToArray())
        {


            if (!timer.HasPassed)
            {
                timer.untilPassAction?.Invoke();
                continue;
            }
            
            timer.onPassedAction?.Invoke();

            if (!timer.repeat)
            {
                _timers.Remove(timer);
            } else
            {
                timer.Restart();
            }
        }
    }

    private void OnDestroy()
    {
        _timers.Clear();
    }

    public class Timer
    {
        private Utils.Timer _innerTimer;
        private float targetTime;
        public Action untilPassAction;
        public Action onPassedAction;
        public bool repeat;

        public bool HasPassed => _innerTimer.HasPassed;
        public float PassedValue => _innerTimer.PassedValue;

        public Timer(Action onPassed, float seconds, bool repeat)
        {
            targetTime = seconds;
            _innerTimer = new Utils.Timer(seconds);
            onPassedAction = onPassed;
            this.repeat = repeat;
            _timers.Add(this);
        }

        public Timer(Action onPassed, Action untilPass, float seconds, bool repeat) : this(onPassed, seconds, repeat)
        {
            untilPassAction = untilPass;
        }

        public void Remove()
        {
            _timers.Remove(this);
        }

        public void Restart()
        {
            _innerTimer = new Utils.Timer(targetTime);
        }
    }
}
