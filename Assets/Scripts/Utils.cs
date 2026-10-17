using UnityEngine;

public static class Utils
{
    public static int BoolToInt(bool boolean)
    {
        return boolean ? 1 : 0;
    }

    public static Vector2 Nearest(Vector2 target, Vector2[] VectorArray)
    {
        Vector2 nearestVector = VectorArray[0];

        foreach (Vector2 currentVector in VectorArray)
        {
            float nearestDistance = Vector2.Distance(nearestVector, target);
            float currentDistance = Vector2.Distance(currentVector, target);
            nearestVector = currentDistance < nearestDistance ? currentVector : nearestVector;
        }
        return nearestVector;
    }

    public class Timer
    {
        private float startTime;
        private float targetSeconds;
        public Timer(float seconds)
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
