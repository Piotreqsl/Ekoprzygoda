using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizerHP : MonoBehaviour
{
    // Start is called before the first frame update

       
    public static Sprite[] sprites;
    public static GameObject guide;
    public static float timer;
    float bckTimer;

    void Start()
    {
        bckTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {


        timer -= Time.deltaTime;

        if( timer < 0)
        {

            timer = bckTimer;
            int r = Random.Range(0, 3);
            guide.GetComponent<SpriteRenderer>().sprite = sprites[r];

        }





    }

    




}
