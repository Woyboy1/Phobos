using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationRenderer : MonoBehaviour
{
    [SerializeField] private GameObject section5;
    [SerializeField] private GameObject section4;
    [SerializeField] private GameObject section1;

    private bool rendered = false;

    private void Start()
    {
        section4.SetActive(false);
        section5.SetActive(false);
    }

    public void ResetScene() // use when the player dies
    {
        section1.SetActive(true);
        section4.SetActive(false);
        section5.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!rendered)
            {
                section1.SetActive(false);
                section4.SetActive(true);
                section5.SetActive(true);
                rendered = true;
            } else
            {
                section1.SetActive(true);
                section4.SetActive(false);
                section5.SetActive(false);
                rendered = false;
            }
        }
    }
}
