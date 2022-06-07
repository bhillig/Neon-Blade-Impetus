using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [Header("Functionality Dependencies")]
    public SceneLoader sceneLoader;
    public ThirdPersonCameraTargetController cameraTargetController;
    public InputInfo info;

    [Header("UI Dependencies")]
    public Button mainMenuButton;
    public Slider sensitivitySlider;
    public Slider dampingSlider;
    

    private float sensitivityMin = 0f;
    private float sensitivityMax = 50f;

    private void Awake()
    {
        gameObject.SetActive(false);
        mainMenuButton.onClick.AddListener(ReturnToMenu);
        info.OptionsMenuDownEvent.AddListener(ToggleMenu);

        sensitivitySlider.maxValue = sensitivityMax;
        sensitivitySlider.minValue = sensitivityMin;
        float val = PlayerPrefs.GetFloat("MouseSensitivity", 20f);
        sensitivitySlider.value = val;
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);

        float damping = PlayerPrefs.GetFloat("MouseDamping", 0.2f);
        dampingSlider.value = damping;
        dampingSlider.onValueChanged.AddListener(ChangeDamping);

        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveListener(ReturnToMenu);
        info.OptionsMenuDownEvent.RemoveListener(ToggleMenu);
        sensitivitySlider.onValueChanged.RemoveListener(ChangeSensitivity);
    }

    private void ChangeSensitivity(float val)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", val);
        cameraTargetController.Sensitivity = val;
    }
    private void ChangeDamping(float val)
    {
        PlayerPrefs.SetFloat("MouseDamping", val);
        cameraTargetController.Damping = val;
    }

    private void ReturnToMenu()
    {
        ToggleMenu();
        sceneLoader.EndSession();
        Cursor.lockState = CursorLockMode.None;
    }

    private void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if(gameObject.activeSelf)
        {
            EnableUIInput(true);
        }
        else
        {
            EnableUIInput(false);
        }
        Cursor.lockState = gameObject.activeSelf ?  CursorLockMode.None : CursorLockMode.Locked;
    }

    private void EnableUIInput(bool val)
    {
        PlayerMovementInputManager.instance.SetEnabled(!val);
        PlayerUIInputManager.instance.SetEnabled(val);
    }
}
