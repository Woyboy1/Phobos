using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        FindAnyObjectByType<PlayerSettings>().Resume();
    }
}
