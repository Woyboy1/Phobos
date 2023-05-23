using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    /// <summary>
    /// this class will be used to set the settings of the "Game" scene using all the static
    /// singletons or other patterns. 
    /// </summary>

    

    private void Awake()
    {
        AudioManager.instance.Stop("MainMenuMusic");
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Stop("MainMenuMusic");
    }
}
