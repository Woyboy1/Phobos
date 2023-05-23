using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickupText;

    [Header("Inventory")]
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI plasticText;
    [SerializeField] private TextMeshProUGUI trashText;

    [SerializeField] private float timer = 2.0f;

    private PlayerInventory inventory;
    private int numOfItems;

    void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        pickupText.gameObject.SetActive(false);
    }

    
    void Update()
    {

    }

    public void UpdateText(int numOfItems, string type) // for interactions
    {
        Color colorWhite = new Color(255, 255, 255, 0.32f); // hard coded value of 0.32 for the transparency. 
        pickupText.color = colorWhite;

        pickupText.text = "+" + numOfItems + " " + type;
        StartCoroutine(TextDisplay(timer));
    }

    IEnumerator TextDisplay(float timer)
    {
        pickupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(timer);
        pickupText.gameObject.SetActive(false);
    }

    // Updating UI for Crafting Table

    public void UpdateMetalText(int metalAmount)
    {
        metalText.text = metalAmount + " Metal";
    }

    public void UpdatePlasticText(int plasticAmount)
    {
        plasticText.text = plasticAmount + " Plastic";
    }

    public void UpdateTrashText(int trashAmount)
    {
        trashText.text = trashAmount + " Trash";
    }

    public void UpdateResource() // for workbench
    {
        metalText.text = inventory.Metal + " Metal";
        plasticText.text = inventory.Plastic + " Plastic";
        trashText.text = inventory.Trash + " Trash";
    }

    public void PurchaseCrowbar()
    {
        if (inventory.Metal >= 3)
        {
            inventory.Metal -= 3;
            inventory.EquipCrowbar();
            AudioManager.instance.Play("CraftingItem");
            UpdateResource();
        } else { AudioManager.instance.Play("CraftingError"); }
    }

    public void PurchaseFlashlightUpgrade()
    {
        if (inventory.Plastic >= 3 && inventory.Trash >= 1)
        {
            inventory.Plastic -= 3;
            inventory.Trash -= 1;
            inventory.EquipFlashlightUpgrade();
            AudioManager.instance.Play("CraftingItem");
            UpdateResource();
        } else { AudioManager.instance.Play("CraftingError"); }
    }

    public void PurchaseMakeshiftBattery()
    {
        if (inventory.Plastic >= 2 && inventory.Metal >= 1)
        {
            inventory.Plastic -= 2;
            inventory.Metal -= 1;
            inventory.EquipMakeshiftBattery();
            AudioManager.instance.Play("CraftingItem");
            UpdateResource();
        } else { AudioManager.instance.Play("CraftingError"); }
    }
}
