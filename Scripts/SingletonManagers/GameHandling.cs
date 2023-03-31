using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandling : MonoBehaviour
{
    // Singleton Pattern
    public static GameHandling instance;

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

    public void PlayerDies() // includes jumpscare
    {
        FindAnyObjectByType<JumpscareManager>().GhoulJumpscare();
    }
}
