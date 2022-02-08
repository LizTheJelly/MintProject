using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnLocation;
    [SerializeField]
    private GameObject prefabObj;
    [SerializeField]
    private float timeInterval;
    [SerializeField]
    private float resetTimeSpawn;
    [SerializeField]
    private int maxSpawn;

    private List<GameObject> listOfObj;
    private float timePassed;

    // Start is called before the first frame update
    // Creates a list to track how many enemies were spawned within the time interval.
    void Start()
    {
        listOfObj = new List<GameObject>();
        timePassed = 0;
    }

    // Update is called once per frame
    // When time is greater than or equal to the set time interval and there is no more than maxSpawn objects,
    // it adds a new gameObject to the game space. If a certain amount of seconds passed (resetTimeSpawn),
    // the list is cleared and begins to spawn more objects again.
    void Update()
    {
        timePassed += Time.deltaTime;
        if ((timePassed >= timeInterval) && (listOfObj.Count != maxSpawn))
        {
            Vector3 position = spawnLocation.transform.position;
            position.z = 0;
            GameObject newObj = GameObject.Instantiate(prefabObj, position, Quaternion.identity);
            listOfObj.Add(newObj);
            timePassed = 0;
        }
        else if (timePassed >= resetTimeSpawn && resetTimeSpawn > 0)
        {
            listOfObj.Clear();
            timePassed = 0;
        }
    }
}
