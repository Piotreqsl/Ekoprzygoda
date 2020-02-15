using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashIdentifier : MonoBehaviour
{

    BoxCollider2D bc;
    public string rodzaj;
    SpriteRenderer sr;
    public string[] acceptedItems;
    string[] acc;
    public Vector3 spawnpoint;
    public SpritePicker s;
    public Vector3 trashcan;
    public bool isOverTrashcan;
    GameObject errorcanvas;
    GameObject wincanvas;

    [Header("Wynik, który wygrywa")]
    public float maxScore;


    GameObject BIO;
    GameObject METAL;
    GameObject PAPIER;
    GameObject SZKLO;
    GameObject ZMIESZANE;
    GameObject ELEKTRO;

    public Button button;

    public static bool respawnNext = true;

    

    String correct;

    public GameObject winCanv;
    public GameObject losCanv;
    public GameObject initCanv;
    public string nazwaLewelaUnikatowa;
    Button resumet;

    public static bool canDrag = true;


    public void openInitial()
    {
        initCanv.SetActive(true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        InitialHelp.CanStart = true;
        Time.timeScale = 0;
        canDrag = false;

        

        Debug.Log("Opened");
    }


    public void closeInitial()
    {
        InitialHelp.wantToClose = true;
        InitialHelp.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        canDrag = true;

        initCanv.GetComponent<Animator>().SetBool("canHide", true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        initCanv.SetActive(false);

    }



    // Start is called before the first frame update
    void Start()
    {
        winCanv.SetActive(false);
        losCanv.SetActive(false);

        respawnNext = true;
        acc = acceptedItems;

        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        trashcan = this.gameObject.transform.position;
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;

        /*
        wincanvas = GameObject.Find("wincanvas");
        errorcanvas = GameObject.FindGameObjectWithTag("ErrorCanvas");
        errorcanvas.GetComponent<Canvas>().enabled = false;
        wincanvas.GetComponent<Canvas>().enabled = false;
        */


        Time.timeScale = 1;

        

        BIO = GameObject.FindGameObjectWithTag("BIO");
        METAL = GameObject.FindGameObjectWithTag("METAL");
        PAPIER = GameObject.FindGameObjectWithTag("PAPIER");
        SZKLO = GameObject.FindGameObjectWithTag("SZKLO");
        ZMIESZANE = GameObject.FindGameObjectWithTag("ZMIESZANE");
        ELEKTRO = GameObject.FindGameObjectWithTag("ELEKTRO");


        if (!PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {
            PlayerPrefs.SetInt(nazwaLewelaUnikatowa, 1);
            openInitial();
            resumet = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();

        }




    }

    void OnTriggerEnter2D(Collider2D b)
    {



        var kosz = Resources.Load<Sprite>("trashcans/" + rodzaj + "_open");
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = kosz;

        DragAndDrop dad = b.GetComponent<DragAndDrop>();
       



    }

    void OnTriggerStay2D(Collider2D b1) {
        

        DragAndDrop dad = b1.GetComponent<DragAndDrop>();

        


        if (!dad.isBeingHeld)
        {

            
            int pos = Array.IndexOf(acceptedItems, b1.name);

            if (pos > -1)
            {

                ScoreCounter.scoreVal += 1;

                Debug.Log("Poprawnie wyrzucony " + b1.name + " do " + rodzaj);
                GameObject trashCorrect = GameObject.FindGameObjectWithTag("trash");
                b1.transform.position = trashcan;
                Destroy(trashCorrect);

                Debug.Log("Wynik to " + ScoreCounter.scoreVal);






            }
            else
            {
                losCanv.SetActive(true);


                respawnNext = false;
                Debug.Log("Niepoprawnie wyrzucony " + b1.name + " do " + rodzaj);
                b1.transform.position = spawnpoint;
                
                ScoreCounter.scoreVal = 0;


               
                //errorcanvas.GetComponent<Canvas>().enabled = true;
                //errorcanvas.GetComponent<Animator>().SetBool("canAnimate", true);
                
                losCanv.GetComponent<Animator>().SetBool("canAnimate", true);




                GameObject smiec = GameObject.FindGameObjectWithTag("smiecimg");
                GameObject kosz = GameObject.FindGameObjectWithTag("koszimg");

                smiec.SetActive(true);
                kosz.SetActive(true);


                Text text = GameObject.Find("TextSuccess").GetComponent<Text>();
                text.text = "wyrzuca się do:";

               



                var smiec_img = Resources.Load<Sprite>("TrashToSpawn/" +b1.name);

                smiec.gameObject.GetComponent<Image>().sprite = smiec_img;

                int bio = Array.IndexOf(BIO.GetComponent<TrashIdentifier>().acceptedItems, b1.name);
                int metale = Array.IndexOf(METAL.GetComponent<TrashIdentifier>().acceptedItems, b1.name);
                int papier = Array.IndexOf(PAPIER.GetComponent<TrashIdentifier>().acceptedItems, b1.name);
                int szklo = Array.IndexOf(SZKLO.GetComponent<TrashIdentifier>().acceptedItems, b1.name);
                int zmiesz = Array.IndexOf(ZMIESZANE.GetComponent<TrashIdentifier>().acceptedItems, b1.name);
                int elektro = Array.IndexOf(ELEKTRO.GetComponent<TrashIdentifier>().acceptedItems, b1.name);


                if (bio > -1)
                {
                    correct = "bio";
                }
                if (metale > -1)
                {
                    correct = "metale";
                }
                if (papier > -1)
                {
                    correct = "papier";
                }
                if (szklo > -1)
                {
                    correct = "szklo";
                }
                if (zmiesz > -1)
                {
                    correct = "zmieszane";
                }
                if (elektro > -1)
                {
                    correct = "elektrosmieci";
                }

                Debug.Log(correct);
                


                var kosz_img = Resources.Load<Sprite>("trashcans/"+ correct + "_closed");
                kosz.gameObject.GetComponent<Image>().sprite = kosz_img;

                GameObject trashActive = GameObject.Find(b1.name);
                Destroy(trashActive);



                Time.timeScale = 0;

            }


            if (ScoreCounter.scoreVal == GameObject.Find("Text").GetComponent<ScoreCounter>().maxScore) {
                ///


                respawnNext = false;
                winCanv.SetActive(true);
                winCanv.GetComponent<Animator>().SetBool("canAnimate", true);

                
                


            }


        }

    }


    void OnTriggerExit2D(Collider2D b1)
    {
        var kosz = Resources.Load<Sprite>("trashcans/" + rodzaj + "_closed");
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = kosz;

        



    }

    void TaskOnClick()
    {

        errorcanvas.GetComponent<Canvas>().enabled = false;
    }


    



    // Update is called once per frame
    void Update()
    {
        if (InitialHelp.finished == true && PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {

            resumet.onClick.AddListener(closeInitial);
            InitialHelp.finished = false;


        }


    }
}
