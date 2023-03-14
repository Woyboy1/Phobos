using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

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
