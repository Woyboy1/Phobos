using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.LowLevel;

public class PaperNote : Interactable
{
    [SerializeField] private GameObject paperNoteUI;
    [SerializeField] private TextMeshProUGUI paperText;

    [SerializeField] private string letterMessage;

    private bool letterOpened = false;
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;

    new void Start()
    {
        base.Start();
        interactableID = "Note";

        playerMovement = FindAnyObjectByType<PlayerMovement>();
        mouseLook = FindAnyObjectByType<MouseLook>();

        InitializeMessage();
    }

    new void Update()
    {
        base.Update();
    }

    private void InitializeMessage()
    {
        paperText.text = letterMessage;
        paperNoteUI.SetActive(false);
    }

    public void PickupLetter()
    {
        AudioManager.instance.Play("Paper");
        if (!letterOpened)
        {
            Open();
        } else
        {
            Close();
        }
    }

    private void Open()
    {
        InitializeMessage();
        paperNoteUI.SetActive(true);
        letterOpened = true;

        mouseLook.CanLook = false;
        playerMovement.CanMove = false;
    }

    private void Close()
    {
        paperNoteUI?.SetActive(false);
        letterOpened = false;

        mouseLook.CanLook = true;
        playerMovement.CanMove = true;
    }

}
