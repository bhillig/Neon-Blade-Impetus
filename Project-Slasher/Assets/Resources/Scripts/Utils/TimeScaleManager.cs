using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager instance;

    private Coroutine currentCorout;
    private void Awake()
    {
        instance = this;
    }

    public void SetTimeScale(float scale, float duration, System.Action callback = null, System.Action frameCallback = null)
    {
        SetTimeScale(scale, duration, 0, callback, frameCallback);
    }

    private IEnumerator CoroutSetTimeScale(
        float scale, 
        float duration,
        float lerpTime, 
        System.Action callback,
        System.Action frameCallback)
    {
        float fixedDelta = Time.fixedDeltaTime;
        float time = 0f;
        float targetFixed = Time.fixedDeltaTime * scale;
        while (time < lerpTime)
        {
            time += Time.unscaledDeltaTime;
            float t = time / lerpTime;
            Time.timeScale = Mathf.Lerp(1, scale, t);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedUnscaledDeltaTime, targetFixed, t);
            frameCallback?.Invoke();
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = scale;
        Time.fixedDeltaTime = targetFixed;
        yield return new WaitForSecondsRealtime(duration);
        time = 0f;
        while (time < lerpTime)
        {
            time += Time.unscaledDeltaTime;
            float t = time / lerpTime;
            Time.timeScale = Mathf.Lerp(scale, 1, t);
            Time.fixedDeltaTime = Mathf.Lerp(targetFixed, Time.fixedUnscaledDeltaTime, t);
            frameCallback?.Invoke();
            yield return new WaitForEndOfFrame();
        }
        Time.fixedDeltaTime = fixedDelta;
        Time.timeScale = 1;
        callback?.Invoke();
    }

    public void SetTimeScale(float scale, float duration, float lerpTime, System.Action callback = null, System.Action frameCallback = null)
    {
        if (currentCorout != null)
            StopCoroutine(currentCorout);
        currentCorout = StartCoroutine(CoroutSetTimeScale(scale, duration,lerpTime, callback, frameCallback));
    }
}
