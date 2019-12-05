using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBases : MonoBehaviour
{

    public bool isPlayerOneBase;
    
    public int startingBaseHealth;
    public int currentBaseHealth;


    void Start()
    {
        currentBaseHealth = startingBaseHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentBaseHealth -= damage;

            if (currentBaseHealth <= 0)
            {
                // Tell end screen that other player won (hence !)
                GameManager.instance.DisplayEndScreen(!isPlayerOneBase);

            }

    }
    
}
