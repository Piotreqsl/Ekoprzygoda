using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePickerAR : MonoBehaviour
{


    public GameObject spawnpointGO;
    public GameObject sprite;

    public void create()
    {
        spawnpointGO = GameObject.FindGameObjectWithTag("Respawn");
        
        sprite.gameObject.tag = "trash";
        Instantiate(sprite, GameObject.FindGameObjectWithTag("Finish").transform);
        //sprite.transform.localScale = new Vector3(0.07f, 0.07f, 1f);
        sprite.transform.position = new Vector3(0,-179,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("trash"));
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
