using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float timeInterval = 8f; [Tooltip("Time til it moves the next scene")]

    [Header("Variables")]
    [SerializeField] private Slider loadingBar;
    [SerializeField] private string sceneName;
    [SerializeField] private float timeModifier = 1f;
    private void Start()
    {
        loadingBar.maxValue = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.value += timeModifier * Time.deltaTime;

        if (loadingBar.value >= timeInterval)
            SceneManager.LoadScene(sceneName);
    }
}
