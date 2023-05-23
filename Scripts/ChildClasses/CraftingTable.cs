using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
    [SerializeField] private GameObject craftingScreenUI;
    private bool tableActive = false;

    new void Start()
    {
        base.Start();
        interactableID = "CraftingTable";
    }

    public void ToggleCraftingTable()
    {
        MouseLook playerLook = FindAnyObjectByType<MouseLook>();
        PlayerMovement playerMovement = FindAnyObjectByType<PlayerMovement>();
        AudioManager.instance.Play("Workbench");

        if (tableActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            craftingScreenUI.SetActive(false);
            tableActive = false;

            playerLook.CanLook = true;
            playerMovement.CanMove = true;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            craftingScreenUI.SetActive(true);
            tableActive = true;

            // Animation:
            playerLook.gameObject.GetComponent<Animator>().Play("New State");

            playerLook.CanLook = false;
            playerMovement.CanMove = false;
        }
    }
}
