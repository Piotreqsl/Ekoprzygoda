using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{

    GameObject light_on;
    bool lightIsOn = false;
    Animator animator;

    ElectricityHandler eh;
    public float timeLeft;
    public float LightTimer;
    

    [Header("Wartości do randoma - tenant")]
    public float timeLeft_val1;
    public float timeLeft_val2;

    
    [Header("Licznik zapalonego swiatla - ile sekund")]
    public float LightTimer_val;
    GameObject timeToWin;
    public GameObject timelefttxt;

    public GameObject gameoverCanvas;
    GameObject maincam;
    GameObject maincanvas;
    // Start is called before the first frame update
    void Start()
    {


        
        lightIsOn = false;
        // light_on = GameObject.Find("block_1/light_on");
        light_on = transform.Find("light_on").gameObject;
        animator = GetComponentInChildren<Animator>();

        timeLeft = Random.Range(timeLeft_val1, timeLeft_val2);
        
        eh = GameObject.Find("_mainCanvas").GetComponent<ElectricityHandler>();
        // LightTimer = 5f;

        gameoverCanvas.SetActive(false);

        this.GetComponent<BoxCollider2D>().enabled = true;

        maincam = GameObject.Find("maincam");
        maincanvas = GameObject.Find("_mainCanvas");


    }

    // Update is called once per frame
    void Update()
    {

        if (maincam.GetComponent<Stopwatch_universal>().isWon || maincanvas.GetComponent<ElectricityHandler>().isLost)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (!maincam.GetComponent<Stopwatch_universal>().isWon) this.GetComponent<BoxCollider2D>().enabled = true;



        timeLeft -= Time.deltaTime;


        if (light_on.GetComponent<SpriteRenderer>().enabled == true && animator.GetBool("walkin") == false && animator.GetBool("idle") == false && animator.GetBool("walkout") == false)
        {

            eh.bar -= 0.0005f;

            /*DLA ARTIST DESIGNERA 
         tutaj to 0.001f    zmieniasz i w zaleznosci od tego bedzie odejmowac szybciej [logiczne mordko]
         wez pod uwage ze jak bedzie 10 pokojow to szybko zleci strasznie

         
         
         */


        }

        if (maincam.GetComponent<Stopwatch_universal>().mustRun) { 

            if (timeLeft < 0)
            {

                timeLeft = Random.Range(3, 16);
               // Debug.Log(timeLeft);

                if (animator.GetBool("walkin") == false && animator.GetBool("idle") == false)
                {

                    if (light_on.GetComponent<SpriteRenderer>().enabled == false)
                    {
                        animator.SetBool("walkin", true);
                        light_on.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }



                if (animator.GetBool("idle") == true)
                {
                    animator.SetBool("walkout", true);
                }




            }
    }
    }


   

    

  void OnMouseDown()
    {
        if(light_on.GetComponent<SpriteRenderer>().enabled == true) { 
        lightIsOn = !lightIsOn;
        light_on.GetComponent<SpriteRenderer>().enabled = !light_on.GetComponent<SpriteRenderer>().enabled;
        }
        if (animator.GetBool("walkin") == true || animator.GetBool("idle") == true)
        {

            gameoverCanvas.SetActive(true);
            gameoverCanvas.GetComponent<Animator>().SetBool("canAnimate", true);

            timeToWin = GameObject.Find("timeToWin");
            //timeToWin
            timeToWin.GetComponent<Text>().text = "zabrakło: " + timelefttxt.GetComponent<Text>().text;
            this.GetComponent<BoxCollider2D>().enabled = false;
            maincanvas.GetComponent<ElectricityHandler>().isLost = true;
            //Debug.Log("PRZEGRANA");
            Time.timeScale = 0;
            
            
        }


        }



}
