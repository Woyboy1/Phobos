using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Keypad : Interactable
{
    [Header("UI")]
    [SerializeField] private GameObject keypadPanel;
    [SerializeField] private TextMeshProUGUI keypadText;

    [Header("Password Logic")]
    [SerializeField] private string passcode; [Tooltip("Must be a number")]

    [Header("Door")]
    [SerializeField] private KeypadDoor[] doors;
    
    bool keypadOpen = false;
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    string passcodeGuess = "";
    int numAdded = 0;
    bool openedDoor = false;


    new void Start()
    {
        base.Start();
        interactableID = "Keypad";
        keypadPanel.SetActive(false);

        playerMovement = FindAnyObjectByType<PlayerMovement>();
        mouseLook = FindAnyObjectByType<MouseLook>();
    }

    new void Update()
    {
        base.Update();
    }

    public void InteractKeypad()
    {
        if (!keypadOpen)
        {
            AudioManager.instance.Play("Beep");
            OpenKeypad();
        } else
        {
            CloseKeypad();
        }
    }

    public void OpenKeypad()
    {
        Cursor.lockState = CursorLockMode.None;
        keypadOpen = true;
        keypadPanel.SetActive(true);

        mouseLook.StopBobbing();
        mouseLook.CanLook = false;
        playerMovement.CanMove = false;
    }

    public void CloseKeypad()
    {
        Cursor.lockState = CursorLockMode.Locked;
        keypadOpen = false;
        keypadPanel.SetActive(false);

        mouseLook.CanLook = true;
        playerMovement.CanMove = true;
    }

    void CheckPasscode()
    {
        if (!openedDoor)
        {
            if (!passcodeGuess.Equals(passcode))
                return;

            // Unlock Door
            AudioManager.instance.Play("Correct");
            AudioManager.instance.Play("Unlock");
            openedDoor = true;

            foreach (KeypadDoor door in doors)
            {
                door.UnlockDoor();
            }
        } 
    }

    void CheckGuesses()
    {
        if (numAdded != 5)
            return;

        AudioManager.instance.Play("Error");
        numAdded = 0;
        passcodeGuess = "";
    }

    void UpdateText()
    {
        keypadText.text = passcodeGuess;
    }

    public void Enter()
    {
        // Checking logic:
        CheckPasscode();
        CheckGuesses();

        if (!passcodeGuess.Equals(passcode))
        {
            AudioManager.instance.Play("Error");
        }

        // Reseting:
        passcodeGuess = "";
        UpdateText();
    }

    #region messy buttons
    public void InputOne()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "1";
        numAdded += 1;

        UpdateText();
    }

    public void InputTwo()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "2";
        numAdded += 1;

        UpdateText();
    }

    public void InputThree()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "3";
        numAdded += 1;

        UpdateText();
    }

    public void InputFour()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "4";
        numAdded += 1;

        UpdateText();
    }

    public void InputFive()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "5";
        numAdded += 1;

        UpdateText();
    }

    public void InputSix()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "6";
        numAdded += 1;

        UpdateText();
    }

    public void InputSeven()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "7";
        numAdded += 1;

        UpdateText();
    }

    public void InputEight()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "8";
        numAdded += 1;

        UpdateText();
    }

    public void InputNine()
    {
        AudioManager.instance.Play("Button");
        passcodeGuess += "9";
        numAdded += 1;

        UpdateText();
    }
    #endregion
}

