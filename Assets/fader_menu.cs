using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fader_menu : MonoBehaviour
{



    [SerializeField] private byte bar =255;
    bool canAnim;
    [SerializeField] private GameObject blck;
    [SerializeField] private byte incr;
   

    // Start is called before the first frame update
    void Start()
    {
        
       
       
        
    }

    // Update is called once per frame
    void Update()
    {




      
        
            if (bar > 0) bar -= incr;

            if (bar == 0)
            {
                bar = 0;
            blck.SetActive(false);

        }

        
       

        


        blck.GetComponent<Image>().color = new Color32(0, 0, 0, bar); 


    }


    

}
