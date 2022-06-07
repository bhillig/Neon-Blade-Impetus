using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerPrefsSlider : MonoBehaviour
{

    public UnityEvent<float> OnSliderValueChanged;

    [Header("Default player prefs val")]
    public string playerPrefsName;
    public bool useDefaultVal;
    public float defaultVal;

    [Header("Slider Values")]
    public float minVal;
    public float maxVal;

    [Header("UI Components")]
    public Slider slider;

    private void Awake()
    {
        slider.minValue = minVal;
        slider.maxValue = maxVal;
        float val = PlayerPrefs.GetFloat(playerPrefsName, useDefaultVal ? defaultVal : (minVal + maxVal / 2));
        slider.onValueChanged.AddListener(SliderChangeListener);
        slider.value = val;
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SliderChangeListener);
    }

    private void SliderChangeListener(float val)
    {
        PlayerPrefs.SetFloat(playerPrefsName, val);
        OnSliderValueChanged?.Invoke(val);
    }

    public void ForceUpdateInvoke()
    {
        OnSliderValueChanged?.Invoke(slider.value);
    }
}
