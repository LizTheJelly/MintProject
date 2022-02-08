using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// LivesHealthManager is a base class that handles the lives and/or health of
// an object, such as the Player, enemies object, or physical object.
public class HealthManager : MonoBehaviour
{
    [SerializeField]
    protected int startingNumHealth;
    protected int numHealth;

    public delegate void InitializeHealth(int currentHealth);
    public InitializeHealth onLevelInitHealth;

    // Start is called before the first frame update
    void Start()
    {
        numHealth = startingNumHealth;
    }

    // GetHealth get and returns the object's health
    public int GetHealth
    {
        get
        {
            return numHealth;
        }
        private set
        { }
    }

    // ApplyDamage() applies damage on the object's health. 
    // The base is set to subtract the player's life when health hits at or below 0.
    public virtual void ApplyDamage(int numDamage)
    {
        numHealth -= numDamage;
        if (numHealth <= 0)
        {
            GameStateManager.PlayerLoseLife();
            numHealth = startingNumHealth;
        }
        if (onLevelInitHealth != null)
        {
            onLevelInitHealth(numHealth);
        }
    }
}
