using System;
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

    public static GameObject Nearest(Vector2 target, GameObject[] objectArray)
    {
        GameObject nearestObject = objectArray[0];
        foreach (GameObject currentObject in objectArray)
        {
            float nearestDistance = Vector2.Distance(nearestObject.transform.position, target);
            float currentDistance = Vector2.Distance(currentObject.transform.position, target);
            nearestObject = currentDistance < nearestDistance ? currentObject : nearestObject;
        }
        return nearestObject;
    }

    public static T Nearest<T>(Vector2 target, T[] componentArray) where T : Component
    {
        T nearestObject = componentArray[0];
        foreach (T currentObject in componentArray)
        {
            float nearestDistance = Vector2.Distance(nearestObject.transform.position, target);
            float currentDistance = Vector2.Distance(currentObject.transform.position, target);
            nearestObject = currentDistance < nearestDistance ? currentObject : nearestObject;
        }
        return nearestObject;
    }

    public static float GetDirectionDegree(Vector2 originPosition, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - originPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }

    //public static int getCurrentFrameIndex(Animator animator)
    //{
    //    AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
    //    AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);

    //    AnimationClip clip = clips[]
    //}

    public class Timer
    {
        private float startTime;
        private float targetSeconds;

        private float Difference => UnityEngine.Time.time - startTime;

        public Timer(float seconds)
        {
            startTime = UnityEngine.Time.time;
            targetSeconds = seconds;
        }

        public bool HasPassed()
        {
            return Difference > targetSeconds;
        }

        public float passedValue()
        {
            return Mathf.Clamp01(Difference / targetSeconds);
        }
    }
}
