using UnityEngine;

public static class Utils
{
    public class Timer
    {
        private float startTime;
        private float targetSeconds;
        Timer(float seconds)
        {
            startTime = UnityEngine.Time.time;
            targetSeconds = seconds;
        }

        public bool HasPassed()
        {
            float difference = UnityEngine.Time.time - startTime;
            return difference > targetSeconds;
        }
    }
}
