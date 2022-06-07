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
    public PlayerPrefsSlider sensitivitySlider;
    public PlayerPrefsSlider dampingSlider;
    public PlayerPrefsSlider sfxSlider;
    public PlayerPrefsSlider musicSlider;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(ReturnToMenu);
        info.OptionsMenuDownEvent.AddListener(ToggleMenu);

        sensitivitySlider.OnSliderValueChanged.AddListener(ChangeSensitivity);
        dampingSlider.OnSliderValueChanged.AddListener(ChangeDamping);
        sfxSlider.OnSliderValueChanged.AddListener(ChangeSFXVolume);
        musicSlider.OnSliderValueChanged.AddListener(ChangeMusicVolume);

        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Start()
    {
        sensitivitySlider.ForceUpdateInvoke();
        dampingSlider.ForceUpdateInvoke();
        sfxSlider.ForceUpdateInvoke();
        musicSlider.ForceUpdateInvoke();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveListener(ReturnToMenu);
        info.OptionsMenuDownEvent.RemoveListener(ToggleMenu);
        sensitivitySlider.OnSliderValueChanged.RemoveListener(ChangeSensitivity);
        dampingSlider.OnSliderValueChanged.RemoveListener(ChangeDamping);
        sfxSlider.OnSliderValueChanged.RemoveListener(ChangeSFXVolume);
        musicSlider.OnSliderValueChanged.RemoveListener(ChangeMusicVolume);
    }

    private void ChangeSensitivity(float val)
    {
        cameraTargetController.Sensitivity = val;
    }
    private void ChangeDamping(float val)
    {
        cameraTargetController.Damping = val;
    }
    private void ChangeSFXVolume(float val)
    {
        FMODMixerManager.instance.SetSFXVolume(val);
    }
    private void ChangeMusicVolume(float val)
    {
        FMODMixerManager.instance.SetMusicVolume(val);
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
