using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        Color colorWhite = new Color(255, 255, 255, 0.32f); // hard coded value of 0.32 for the transparency. 
        Color colorRed = new Color(255, 0, 0, 0.32f);

        currentBattery = playerInteractionsScript.StartingBattery;

        if (currentBattery > 100)
        {
            currentBattery = 100;
        }

        if (currentBattery <= 25)
            batteryText.color = colorRed;
        else
            batteryText.color = colorWhite;

        batteryText.text = currentBattery.ToString("0");

    }
}
