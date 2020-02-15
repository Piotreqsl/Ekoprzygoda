using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    bool canT;
    public float timer;
    public byte bar;
    public float fTimer;
    public GameObject blck;
    public byte incr;
    public bool complete;

    // Start is called before the first frame update
    void Start()
    {
        canT = false;
        bar = 0;
        fTimer = timer;



    }

    // Update is called once per frame
    void Update()
    {
        

        if(!canT)
        {
            bar = 0;
            
        } 

        if(canT)
        {
            
            if(bar < 255) bar += incr;

            if (bar == 255)
            {
                bar = 255;
                SceneManager.LoadScene(1);
                
            }
            


        }


        blck.GetComponent<Image>().color = new Color32(0,0,0,bar);;

    }


    public void toMenu() {

        Debug.Log("debug");
        canT = true;

    }

}
