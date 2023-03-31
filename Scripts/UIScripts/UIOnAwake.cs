using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnAwake : MonoBehaviour
{
    // For the crafting table
    private void OnEnable()
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();
        UIInventory UIInventory = FindAnyObjectByType<UIInventory>();

        UIInventory.UpdateMetalText(inventory.Metal);
        UIInventory.UpdatePlasticText(inventory.Plastic);
        UIInventory.UpdateTrashText(inventory.Trash);
        
    }
}
