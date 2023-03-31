using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaBar : MonoBehaviour
{
    [SerializeField] private float startingStamina;
    [SerializeField] private float staminaDrain = 4f;
    [SerializeField] private float staminaRegen = 1.0f;
    // [SerializeField] private float cooldownTimer = 3.0f;
    [SerializeField] private Slider staminaBar;

    private PlayerMovement playerMovementScript; 
    private float maxStamina;

    public float StartingStamina
    {
        get { return startingStamina; }
    }

    void Start()
    {
        maxStamina = staminaBar.maxValue;
        playerMovementScript = GetComponent<PlayerMovement>();

    }

    
    void Update()
    {
        if (playerMovementScript.CanSprint)
        {
            if (playerMovementScript.IsMoving && playerMovementScript.IsSprinting)
            {
                DecreaseEnergy();
            } else
            {
                IncreaseEnergy();
            }

            return;
        }  else
        {
            playerMovementScript.CanSprint = true;
        }
    }

    private void DecreaseEnergy()
    {
        if (startingStamina != 0)
            startingStamina -= staminaDrain * Time.deltaTime;

        if (startingStamina <= 0)
        {
            playerMovementScript.CanSprint = false;
            playerMovementScript.RevertNormalSpeed();
        } 

        UpdateSlider();
    }

    private void IncreaseEnergy()
    {
        if (startingStamina >= maxStamina)
            startingStamina = maxStamina;

        startingStamina += staminaRegen * Time.deltaTime;

        UpdateSlider();
    }

    private void UpdateSlider()
    {
        staminaBar.value = startingStamina;
    }

    public void DrainStamina(float value)
    {
        startingStamina -= value;
        UpdateSlider();
    }
}
