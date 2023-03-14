using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBatteryUpdater : MonoBehaviour
{
    /// <summary>
    /// Basic script to update the battery percentage display.
    /// </summary>

    public static UIBatteryUpdater instance;

    public TextMeshProUGUI batteryText;

    private PlayerInteractions playerInteractionsScript;
    private float currentBattery;

    private void Start()
    {
        playerInteractionsScript = FindObjectOfType<PlayerInteractions>();
        UpdateBatteryPercentage();
    }
    public void UpdateBatteryPercentage()
    {
        currentBattery = playerInteractionsScript.StartingBattery;
        batteryText.text = currentBattery.ToString("0");
    }
}
