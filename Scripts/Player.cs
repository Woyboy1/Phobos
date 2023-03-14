using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// This is the general stats of the player. Every other play managing scripts are under
    /// this GameObject. This is to generally handle the simple stuff such as health and 
    /// (maybe) stamina. If the user were to ever kill the player, this is the place to do it.
    /// </summary>


    [Header("General Stats")]
    [SerializeField] private int health = 1;
    [SerializeField] private int maxHealth = 3;

    void Start()
    {

    }

    
    void Update()
    {
        
    }

    public void DecreaseHealth(int value)
    {
        health -= value;

        if (health <= 0)
        {
            Debug.Log("Player died");
        }
    }

    public void IncreaseHealth(int value)
    {
        health += value;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}
