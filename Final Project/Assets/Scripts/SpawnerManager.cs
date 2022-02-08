using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    GameObject ObjSpawnPoint;

    // Update() checks if the player recently lost a life. If so, player is placed
    // back at player's spawn point.
    void Update()
    {
        bool spawnPlayer = GameStateManager.GetSpawnBool;
        if (spawnPlayer)
        {
            GameStateManager.GetSpawnBool = false;
            ReturnToSpawnPoint();
        }
    }

    // Returns the current object back to its spawn point object's location.
    public void ReturnToSpawnPoint()
    {
        transform.position = ObjSpawnPoint.transform.position;
    }
}
