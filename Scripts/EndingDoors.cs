using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDoors : Interactable
{
    new void Start()
    {
        base.Start();
        interactableID = "Ending";
    }

    new void Update()
    {
        base.Update();
    }

    public void InitiateEnding()
    {
        AudioManager.instance.Play("Unlock");
        SceneManager.LoadScene("EndingScreen");
        Cursor.lockState = CursorLockMode.None;
    }
}
