using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerInteractions : MonoBehaviour
{
    /// <summary>
    /// This is a key component of the player body. The script communicates with multiple scripts and
    /// objects to perform effective actions. How interactions are done is through raycasting every frame. 
    /// While this may not be the most efficient, it was the fastest way for me to implement other than 
    /// some other method. 
    /// 
    /// Flashlights are also exceptionally handled here to manage batteries and when to refill them.
    /// 
    /// The raycast will only respond to the layermask of "Interactable" effectively leaving out the 
    /// environment. For performance, everything is carefully planned out to exercise best coding practices
    /// 
    /// Summary:
    /// Base component for all interactions within the player, interactions are handled here.
    /// 
    /// </summary>

    [Header("Raycasting Neccesities")]
    [SerializeField] private float distanceReach = 5.0f;
    [SerializeField] private float scareRaycastReach = 3.0f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private LayerMask scareMask;

    [Header("Flashlight")]
    [SerializeField] private GameObject flashlightObject;
    [SerializeField] private Light flashlightLightComponent;
    [SerializeField] private float startingBattery = 100f;
    [SerializeField] private float batteryDrainRate = 0.65f;

    [Header("Audio")]
    [SerializeField] private AudioClip sfxFlashlight;
    [SerializeField] private AudioClip sfxDoorBudge;

    [Header("Outline Text")]
    [SerializeField] private GameObject interactionText;
    public float StartingBattery
    {
        get { return startingBattery; }
    }

    private bool flashlightActive = true;
    private Camera cam;
    private AudioSource source;
    private bool hasKey = false;
    private bool hasBattery = true;

    void Start()
    {
        cam = Camera.main;
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        InteractableFinder();
        ToggleFlashlight();


        if (hasBattery)
        {
            if (flashlightActive)
            {
                DrainBattery();
            }
        }
    }

    #region Raycasting and Interaction
    void InteractableFinder()
    {
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.transform.forward * distanceReach, Color.red);

        // Raycasting for interactables:
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distanceReach, interactableMask))
        {
            EnableText();
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetStringID(hit);
            }
        }
        else
        {
            DisableText();
        }

        // Raycasting for jumpscares:
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, scareRaycastReach, scareMask))
        {
            if (hit.collider.GetComponent<JumpscareObject>())
            {
                GetScareID(hit);
            }
        }
    }

    // --------- Important: Finding the object and working with it---------------- 
    void GetStringID(RaycastHit hit)
    {
        Interactable interactableObject = hit.collider.GetComponent<Interactable>();
        string targetID = interactableObject.InteractableID;

        switch (targetID)
        {
            case "Door":
                interactableObject.GetComponent<Door>().OpenDoor();
                break;

            case "Key":
                interactableObject.GetComponent<Key>().PickUpKey();
                hasKey = true;
                break;

            // Locked Door ---------------------------------------------------------
            case "LockedDoor":
                if (hasKey)
                {
                    interactableObject.GetComponent<LockedDoor>().OpenDoor(hasKey);
                    hasKey = false;
                }
                else
                {
                    source.PlayOneShot(sfxDoorBudge);
                    
                }
                break;

            // BATTERY ---------------------------------------------------------
            case "Battery":
                interactableObject.GetComponent<BatteryRecharger>().Recharge();
                FindObjectOfType<UIBatteryUpdater>().UpdateBatteryPercentage();
                break;

            // CONTAINER ------------------------------------------------------
            case "Container":
                Container container = hit.collider.GetComponent<Container>();

                // adding these 2 into the method 
                string itemType = container.Item;
                int itemNum = container.ItemNum;

                if (container.HasItem())
                {
                    container.OpenContainer();
                    AddInventoryItem(itemType, itemNum);
                    FindAnyObjectByType<UIInventory>().UpdateText(itemNum, FilterStringType(itemType));
                }
                
                AudioManager.instance.Play("ContainerOpen");
                container.gameObject.layer = LayerMask.NameToLayer("Default");
                break;
            
            // Crafting Table ----------------------------------------------
            case "CraftingTable":
                interactableObject.GetComponent<CraftingTable>().ToggleCraftingTable();
                break;

            // Barricade ----------------------------------
            case "Barricade":
                if (GetComponentInParent<PlayerInventory>().HasCrowbar)
                {
                    interactableObject.GetComponent<Barricade>().DestroyBarricadeWithCrowbar();
                } else
                {
                    interactableObject.GetComponent<Barricade>().DestroyBarricade(); // with your hands
                }
                break;

            // Note ----------------------------------------
            case "Note":
                interactableObject.GetComponent<PaperNote>().PickupLetter();
                break;

            default:
                Debug.LogWarning("PlayerInteractions: Cannot find ID or is object not assigned correct ID?");
                break;

        }
    }

    void GetScareID(RaycastHit hit)
    {
        JumpscareObject scareObject = hit.collider.GetComponent<JumpscareObject>();

        scareObject.StaticObjectScarePlayer();
    }
    #endregion

    #region Battery and Flashlight
    public void ToggleFlashlight()
    {
        if (hasBattery)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (flashlightActive)
                {
                    FlashlightOff();
                    return;
                }

                flashlightObject.SetActive(true);
                source.PlayOneShot(sfxFlashlight);
                flashlightActive = true;
            }
        }
    }

    void FlashlightOff()
    {
        flashlightActive = false;
        flashlightObject.SetActive(false);
        source.PlayOneShot(sfxFlashlight);
    }

    void DrainBattery()
    {
        BatteryMaxLimiter();

        startingBattery -= batteryDrainRate * Time.deltaTime;
        FindObjectOfType<UIBatteryUpdater>().UpdateBatteryPercentage(); // update text

        if (startingBattery <= 0)
        {
            startingBattery = 0;
            hasBattery = false;
            FlashlightOff();
        }
    }

    public void RechargeBattery(float value)
    {
        BatteryMaxLimiter();

        if (value > 100)
        {
            value = 100;
        }
        else if (value < 0)
        {
            Debug.LogWarning("Cannot recharge battery less than 0, is this a mistake?");
        }

        hasBattery = true;
        startingBattery += value;
    }

    void BatteryMaxLimiter()
    {
        if (startingBattery > 100)
        {
            startingBattery = 100;
        }
    }

    public void UpgradeFlashlight(float rangeBuff, float intensity)
    {
        flashlightLightComponent.intensity += intensity;
        flashlightLightComponent.range += rangeBuff;
    }

    public void InsertMakeShiftBattery(float value)
    {
        RechargeBattery(value);
    }

    #endregion

    #region UI Text
    public void EnableText()
    {
        interactionText.SetActive(true);
    }

    public void DisableText()
    {
        interactionText.SetActive(false);
    }
    #endregion

    #region Inventory
    public void AddInventoryItem(string type, int value)
    {
        FindAnyObjectByType<PlayerInventory>().AddResourceItem(type, value);
    }
    #endregion

    #region Internal Background

    private string FilterStringType(string type)
    {
        string output = type;

        switch(output)
        {
            case "itemMetal":
                output = "Metal";
                break;
            case "itemPlastic":
                output = "Plastic";
                break;
            case "itemTrash":
                output = "Trash";
                break;
            case "itemWood":
                output = "Wood";
                break;
            default:
                Debug.Log("Failed to filter");
                break;
        }

        return output;
    }
    #endregion
}
