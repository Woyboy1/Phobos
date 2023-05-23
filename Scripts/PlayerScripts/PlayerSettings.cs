using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider masterVolumeSlider;

    public static bool GameIsPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;

            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void AdjustMouseSensitivity()
    {
        FindAnyObjectByType<MouseLook>().MouseSensitivity = mouseSensitivitySlider.value;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AudioManager.instance.Play("Click");


        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void AdjustVolumeSettings()
    {
        float volumeValue = masterVolumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        AudioListener.volume = volumeValue;
    }
}
