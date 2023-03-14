using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFpsLimiter : MonoBehaviour
{

    private void Awake()
    {
        // Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = 60;
    }
}
