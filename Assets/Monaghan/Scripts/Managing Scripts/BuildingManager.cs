using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingManager : MonoBehaviour
{

    public bool isPlayerOneBuilding;
    public GameObject techButton;
    public GameObject barracksButton;

    public GameObject techPrefab;
    public GameObject barracksPrefab;

    
    public int startingBuildingHealth;
    public int currentBuildingHealth;

    public bool buildingActive;

    private GameObject currentlySpawnedBuilding;

    public TextMeshPro healthText;
    
    private void Start()
    {
        techButton.GetComponent<SpawnTechBuilding>().OnButtonPush += SpawnTechBuilding;
        barracksButton.GetComponent<SpawnBarracksBuilding>().OnButtonPush += SpawnBarracksBuilding;
        
    }

    private void OnDestroy()
    {
        techButton.GetComponent<SpawnTechBuilding>().OnButtonPush -= SpawnTechBuilding;
        barracksButton.GetComponent<SpawnBarracksBuilding>().OnButtonPush -= SpawnBarracksBuilding;
    }


    public void TakeDamage(int damage)
    {

        if (buildingActive)
        {

            currentBuildingHealth -= damage;

            if (currentBuildingHealth <= 0)
            {
                if (currentlySpawnedBuilding.name.Contains("Tech"))
                {
                    DestroyTechBuilding();
                }
                else
                {
                    DestroyBarracksBuilding();
                }

                currentBuildingHealth = 0;
                currentlySpawnedBuilding = null;

            }
            else
            {
                healthText.text = currentBuildingHealth.ToString();
            }

        }

    }
    
    
    
    

    void ToggleButtons(bool toggle)
    {
        techButton.SetActive(toggle);
        barracksButton.SetActive(toggle);
        healthText.gameObject.SetActive(!toggle);
    }


    void SpawnTechBuilding()
    {


        // Check if this button is owned by this player
        if (GameManager.instance.isPlayerOneTurn == isPlayerOneBuilding)
        {   
            // Spawn the building graphic in my position / rotation
            currentlySpawnedBuilding = Instantiate(techPrefab, transform.position, transform.rotation) as GameObject;
            currentlySpawnedBuilding.transform.SetParent(transform);
            
            // Add 1 to the tech amount of this player
            if (isPlayerOneBuilding)
            {
                BoardManager.instance.playerOneCurrentTechAmount++;
            }
            else
            {
                BoardManager.instance.playerTwoCurrentTechAmount++;
            }
            
            // Set building health
            currentBuildingHealth = startingBuildingHealth;

            // Disable buttons and log that there is now a building
            ToggleButtons(false);
            buildingActive = true;
            healthText.text = currentBuildingHealth.ToString();
        }
        

    }
    
    void SpawnBarracksBuilding()
    {

     // Check if this button is owned by this player
        if (GameManager.instance.isPlayerOneTurn == isPlayerOneBuilding)
        {   
            // Spawn the building graphic in my position / rotation
            currentlySpawnedBuilding = Instantiate(barracksPrefab, transform.position, transform.rotation) as GameObject;
            currentlySpawnedBuilding.transform.SetParent(transform);
            
            // Add 1 to the army size of this player
            if (isPlayerOneBuilding)
            {
                BoardManager.instance.playerOneCurrentArmySize++;
            }
            else
            {
                BoardManager.instance.playerTwoCurrentArmySize++;
            }
            
            // Set building health
            currentBuildingHealth = startingBuildingHealth;
            
            // Disable buttons and log that there is now a building
            ToggleButtons(false);
            buildingActive = true;
            healthText.text = currentBuildingHealth.ToString();

        }
        
        

    }


    void DestroyTechBuilding()
    {
        
        // Destroy the building graphic
        Destroy(currentlySpawnedBuilding);
        
        // Minus 1 from the tech amount of this player
        if (isPlayerOneBuilding)
        {
            BoardManager.instance.playerOneCurrentTechAmount--;
        }
        else
        {
            BoardManager.instance.playerTwoCurrentTechAmount--;
        }
        
        // Disable buttons and log that there is now a building
        ToggleButtons(true);
        buildingActive = false;
        
    }
    
    void DestroyBarracksBuilding()
    {
        
        // Destroy the building graphic
        Destroy(currentlySpawnedBuilding);
        
        // Minus 1 from the army size of this player
        if (isPlayerOneBuilding)
        {
            BoardManager.instance.playerOneCurrentArmySize--;
        }
        else
        {
            BoardManager.instance.playerTwoCurrentArmySize--;
        }
            
        // Disable buttons and log that there is now a building
        ToggleButtons(true);
        buildingActive = false;
        
    }
    
    
}
