using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IAdd<int>
{
    [Tooltip("How many of each of items can you hold.")]
    [SerializeField] private int maxResourceCount = 7;

    [Header("Flashlight Upgrade Statistics")]
    [SerializeField] private float rangeBuff = 0.5f;
    [SerializeField] private float intensityBuff = 0.5f;
    [SerializeField] private float makeshiftBatteryRecharge = 10.0f;

    private int metal;
    private int plastic;
    private int trash;

    private bool hasCrowbar = false;
    private bool hasFlashlightUpgrade = false;
    public int Metal
    {
        get { return metal; }
        set { metal = value; }
    }

    public int Plastic
    {
        get { return plastic; }
        set { plastic = value; }
    }

    public int Trash
    {
        get { return trash; }
        set { trash = value; }
    }

    public int MaxResourceCount
    {
        get { return maxResourceCount; }
        set { maxResourceCount = value; }
    }

    public bool HasCrowbar
    {
        get { return hasCrowbar; }
        set { HasCrowbar = value; }
    }

    public bool HasFlashlightUpgrade
    {
        get { return hasFlashlightUpgrade; }
        set { hasFlashlightUpgrade = value; }
    }

    // Adding total items -- but unneccessary 
    public void Add(int id)
    {

    }

    private void Update()
    {

    }

    #region Inventory Resources
    public void AddResourceItem(string type, int amount)
    {
        if (amount < 0) { return; }
        string typeResult = ProcessResourceType(type).ToLower();
        UpdateInventoryResource(typeResult, amount);
        LimitResourceCount();
    }

    private string ProcessResourceType(string input) // Filtering the string
    {
        string output = "";
        switch (input)
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
            default:
                Debug.LogWarning("Failed to filter");
                break;
        }
        return output;
    }

    private void UpdateInventoryResource(string type, int amount) // Updating Resource Data
    {
        if (amount < 0) { return; }
        switch (type)
        {
            case "metal":
                metal += amount;
                break;
            case "plastic":
                plastic += amount;
                break;
            case "trash":
                trash += amount;
                break;
            default:
                Debug.Log("Failed to Update Resource");
                break;
        }
    }

    public void ClearInventory()
    {
        metal = 0;
        plastic = 0;
        trash = 0;
    }

    private void LimitResourceCount()
    {
        if (metal > maxResourceCount) { metal = maxResourceCount; }
        if (plastic > maxResourceCount) { plastic = maxResourceCount; }
        if (trash > maxResourceCount) {  trash = maxResourceCount; }
    }
    #endregion

    #region Inventory Tools

    public void EquipCrowbar()
    {
        hasCrowbar = true;
        Debug.Log("Crowbar Equipped");
    }

    public void EquipFlashlightUpgrade()
    {
        FindAnyObjectByType<PlayerInteractions>().UpgradeFlashlight(rangeBuff, intensityBuff);
    }

    public void EquipMakeshiftBattery()
    {
        FindAnyObjectByType<PlayerInteractions>().InsertMakeShiftBattery(makeshiftBatteryRecharge);
    }

    #endregion
}
