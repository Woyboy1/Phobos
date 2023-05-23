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
    [SerializeField] private Image fillBar; [Tooltip("Remains a holder for the fillbar. The only purpose is to change color")]

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
            }
            else
            {
                IncreaseEnergy();
            }

            return;
        }
        else
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
        Color colorWhite = new Color(255, 255, 255, 0.32f); // hard coded value of 0.32 for the transparency. 
        Color colorRed = new Color(255, 0, 0, 0.32f);
        staminaBar.value = startingStamina;

        if (startingStamina <= (staminaBar.maxValue / 4))
            fillBar.color = colorRed;
        else fillBar.color = colorWhite;
    }

    public void DrainStamina(float value)
    {
        startingStamina -= value;
        UpdateSlider();
    }
}
