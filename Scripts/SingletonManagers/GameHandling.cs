using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandling : MonoBehaviour
{
    // Singleton Pattern
    public static GameHandling instance;

    private const string gameScene = "Game";
    private const string mainMenuScene = "MainMenu";
    private const string loadingScreenScene = "LoadingScreen";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {

    }

    public void PlayerDiesToGhoul() // includes jumpscare
    {
        FindAnyObjectByType<JumpscareManager>().GhoulJumpscare();
        PlayerReset();
    }

    public void PlayerDiesToZombie()
    {
        FindAnyObjectByType<JumpscareManager>().ZombieJumpscare();
        PlayerReset();
    }

    public void PlayerReset()
    {
        FindAnyObjectByType<PlayerInventory>().ClearInventory();

        // Audio
        AudioManager.instance.Stop("Ambience5");
        AudioManager.instance.Stop("MainMenuMusic");
        AudioManager.instance.Stop("Ambience4");
        AudioManager.instance.Stop("Ambience3");
    }

    public void UIMainMenuQuit()
    {
        Application.Quit();
    }

    public void UIMainMenuStart()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(loadingScreenScene);
        // send user to loading screen when clicked by button
    }

    public void UILoadingScreen()
    {
        SceneManager.LoadScene(gameScene);
        Debug.Log("Loading game");
        // send user to game scene after loading
    }
}
