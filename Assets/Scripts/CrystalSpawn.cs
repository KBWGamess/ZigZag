using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawn : MonoBehaviour
{
    public GameObject crystal;
    public Transform spawnLocation;

    [Range(1, 100)]public int spawnRate;

    void Start()
    {
        int random = Random.Range(1, 100);

        if(random < spawnRate)
        {
            GameObject newCrystal = Instantiate(crystal, spawnLocation.position, Quaternion.identity);
            newCrystal.transform.SetParent(spawnLocation);
        }
    }


    void Update()
    {
        
    }
}
