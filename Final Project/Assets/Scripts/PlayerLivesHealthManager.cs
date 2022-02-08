using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesHealthManager : HealthManager
{
    // If data was saved, set current player's health to saved health number.
    void Start()
    {
        if (PlayerPrefs.HasKey("playerHeath"))
        {
            numHealth = PlayerPrefs.GetInt("playerHealth");
        }
        else
        { 
            // There is no save data.
            numHealth = startingNumHealth;
        } 
    }
}
