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

        if (currentBattery > 100)
        {
            currentBattery = 100;
        }

        if (currentBattery <= 25)
            batteryText.color = Color.red;
        else
            batteryText.color = Color.white;

        batteryText.text = currentBattery.ToString("0");
    }
}
