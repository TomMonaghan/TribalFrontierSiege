using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainBases : MonoBehaviour
{

    public bool isPlayerOneBase;
    
    public int startingBaseHealth;
    public int currentBaseHealth;

    public TextMeshPro mainBaseHealthText;
    public TextMeshPro mainBase2HealthText;

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
            
            

            mainBaseHealthText.text = currentBaseHealth.ToString();
            mainBase2HealthText.text = currentBaseHealth.ToString();
            //mainBaseHealthText.text = currentBaseHealth.ToString();

    }
    
}
