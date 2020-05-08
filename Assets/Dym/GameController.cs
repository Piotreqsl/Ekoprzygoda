using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    
    
    public GameObject smokePrefab;
    public GameObject spawn;
    public float respawnTime = 1.0f;
    public int x;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(asteroidWave());

    }

    // Update is called once per frame
    void Update()
    {
        /*
        x++;
        if (x == 120)
        {
            spawnEnemy();
            x = 0;
        }*/
    }
    private void spawnEnemy()
    {
        Debug.Log("spawn");
        
        
            Instantiate(smokePrefab, spawn.transform.position, spawn.transform.rotation);
        
    }
    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
    
}
