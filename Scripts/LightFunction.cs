using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFunction : MonoBehaviour
{
    [SerializeField] private bool isFlickering = false;
    [SerializeField] private float flickerInterval = 1.0f;
    [SerializeField] private Light lightSource;

    void Start()
    {
        lightSource.enabled = true;
        lightSource.gameObject.SetActive(true);

        StartCoroutine(InitializeLight());
    }

    void Update()
    {
        
    }

    IEnumerator InitializeLight()
    {
        if (isFlickering == true)
        {
            lightSource.enabled = false;
            yield return new WaitForSeconds(flickerInterval);
            lightSource.enabled = true;
            yield return new WaitForSeconds(flickerInterval - flickerInterval + 0.5f);
            StartCoroutine(InitializeLight());
        }
    }
}
