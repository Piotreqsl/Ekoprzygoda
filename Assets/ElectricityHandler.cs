using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityHandler : MonoBehaviour
{

    Image electricityBar;
    public float bar = 1f;
    public GameObject gameoverCanvas;
    public bool isLost;

    public GameObject timeToWin;
    public GameObject timelefttxt;

    // Start is called before the first frame update
    void Start()
    {
        electricityBar = GameObject.Find("electricityBar").GetComponent<Image>();

        gameoverCanvas.SetActive(false);

        Time.timeScale = 1;
        isLost = false;
    }



    


    // Update is called once per frame
    void Update()
    {
        electricityBar.fillAmount = bar;


        if (bar < 0) {
            bar = 0f;

            
            gameoverCanvas.SetActive(true);
            gameoverCanvas.GetComponent<Animator>().SetBool("canAnimate", true);
            isLost = true;
            Time.timeScale = 0;

            timeToWin.GetComponent<Text>().text = "zabrakło: "+timelefttxt.GetComponent<Text>().text;


        }

    }
}
