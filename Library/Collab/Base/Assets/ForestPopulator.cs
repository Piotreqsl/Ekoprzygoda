using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class ForestPopulator : MonoBehaviour
{
    private const string V = "/Assety/Resources/TrashToSpawn";
    public GameObject sprite;
    
    

    SpriteRenderer sr;
    public String[] arr;
    public int numberOfPositions;
    public static int numberOfPositionsSecond;


    public int timeToRespawnNewOne;
    public float czasGry;

    float gameTime;

    Button btn;

    Button help;

    GameObject[] goList;
    public GameObject[] vectorki;
    public static GameObject[] vectorki2;
    public static int dlugosc;
    public static int[] used;

   

    float timeleft;
    float timeleftsave;
    Text czasgry;
    Text przedmiotyLeft;

    string s;
    string m;

    public GameObject helpCanv;
    public GameObject winCanv;
    public GameObject losCanv;
    public GameObject initCanv;

    public string nazwaLewelaUnikatowa;

    SetterPause sp;

   

   


    public static GameObject spriteSecond;
    

    public static int numberOfItems;// Liczba itemków na planszy
    int counterPomocniczy = 1;// do funkcji na jednego

    public static bool gameIsActive = true;
    public static bool hasDefeated = false;

    Button resumet;
    public static void create(int number)
    {

        numberOfItems = number;

        for (int i = 0; i < number; i++)
        {

            Random random = new Random();
            int rand = random.Next(0, dlugosc);

            int pos = Array.IndexOf(used, rand);

            while (pos > -1)
            {
                rand = random.Next(0, dlugosc);
                pos = Array.IndexOf(used, rand);
            }

            used[i] = rand;

            Debug.Log(rand +  " dla int rownego "  + i );

            GameObject test = vectorki2[rand];
            Vector3 wektor = test.transform.position;

            


            spriteSecond.gameObject.tag = "trash";
            Instantiate(spriteSecond);
            spriteSecond.transform.position = wektor;

        }
    }

    public  void populateNewOne()
    {

        if (gameIsActive == true && numberOfItems > 0)
        {

            numberOfItems++;

            if (numberOfPositions + counterPomocniczy > dlugosc)
            { 
                counterPomocniczy = 1;
            }



            Random random = new Random();
            int rand = random.Next(0, dlugosc);

            int pos = Array.IndexOf(used, rand);

            while (pos != -1)
            {
                rand = random.Next(0, dlugosc);
                pos = Array.IndexOf(used, rand);
            }


            used[numberOfPositions + counterPomocniczy] = rand;
            counterPomocniczy++;


            GameObject test = vectorki2[rand];
            Vector3 wektor = test.transform.position;


            spriteSecond.gameObject.tag = "trash";
            Instantiate(spriteSecond);
            spriteSecond.transform.position = wektor;

            Debug.Log("Zpopulowało nowego itemka pozdro oo");

        }


    }




    public void openHelp()
    {

        helpCanv.SetActive(true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        SetterPause.CanStart = true;
        Time.timeScale = 0;
        gameIsActive = false;
        Debug.Log("Opened");


    }

    public void closeHelp()
    {
        SetterPause.wantToClose = true;
        SetterPause.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        gameIsActive = true;
        helpCanv.GetComponent<Animator>().SetBool("canHide", true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        helpCanv.SetActive(false);

    }


    public void openInitial()
    {
        initCanv.SetActive(true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        InitialHelp.CanStart = true;
        Time.timeScale = 0;

        gameIsActive = false;

        Debug.Log("Opened");
    }


    public void closeInitial()
    {
        InitialHelp.wantToClose = true;
        InitialHelp.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        gameIsActive = true;

        initCanv.GetComponent<Animator>().SetBool("canHide", true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        initCanv.SetActive(false);

    }



    // Start is called before the first frame update
    void Start()
    {




       

        gameIsActive = true;
        Time.timeScale = 1;

        helpCanv.SetActive(false);
        winCanv.SetActive(false);
        losCanv.SetActive(false);
        initCanv.SetActive(false);

        // Array na ilosc respawnów i sprite'y
        vectorki = GameObject.FindGameObjectsWithTag("Respawn");
        vectorki2 = vectorki;
        dlugosc = vectorki.Length;
        spriteSecond = sprite;
        numberOfPositionsSecond = numberOfPositions;

        gameTime = czasGry;

        help = GameObject.Find("ButtonHelp").GetComponent<Button>();



        // Miejsca w użyciu
        used = new int[dlugosc + 1];

        czasgry = GameObject.Find("czasGry").GetComponent<Text>();
        przedmiotyLeft = GameObject.Find("przedmioty").GetComponent<Text>();


       



        /// Spawnienie itemków
        try
        {
            goList = GameObject.FindGameObjectsWithTag("trash");
            foreach (GameObject trashek in goList)
            {
                Destroy(trashek);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        create(numberOfPositions);

       

        /// Ustawianie czasu
        timeleft = (float)timeToRespawnNewOne;
        timeleftsave = (float)timeToRespawnNewOne;

       


      
        if (!PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {
            PlayerPrefs.SetInt(nazwaLewelaUnikatowa, 1);
            openInitial();
            resumet = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();

        }
       
            
        
            
        
        


    }






    public void Victory()
    {

     

        winCanv.SetActive(true);
        winCanv.GetComponent<Animator>().SetBool("canAnimate", true);




    }

    public void Defeat()
    {


        losCanv.SetActive(true);
        losCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        hasDefeated = true;
        czasgry.GetComponent<Text>().text = "00:00";

    }



    



    // Update is called once per frame
    void Update()


    {

        if (InitialHelp.finished == true && PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {

            resumet.onClick.AddListener(closeInitial);
            InitialHelp.finished = false;


        }




        przedmiotyLeft.text = "Pozostało: " + numberOfItems;



        if (gameIsActive)
        {
            czasGry -= Time.deltaTime;
        }

        float minutes = Mathf.Floor(czasGry / 60);
        float seconds = Mathf.RoundToInt(czasGry % 60);

        if (minutes < 10)
        {
            m = "0" + minutes.ToString();
        }
        else
        {
            m = minutes.ToString();
        }

        if (seconds < 10)
        {
            s = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            s = Mathf.RoundToInt(seconds).ToString();
        }


        czasgry.GetComponent<Text>().text = m + ":" + s;



        if (gameIsActive) { Debug.Log("Gra aktywana"); }



        if (czasGry < 0 )
        {
            Defeat();
            gameIsActive = false;
            Time.timeScale = 0;



        }

        


        if (numberOfItems == 0 )
        {
            Victory();
            


            gameIsActive = false;
            Time.timeScale = 0;


        }



        if (gameIsActive)
        {

            /// Respienie nowego itemka
            timeleft = timeleft - Time.deltaTime;
            if (timeleft < 0)
            {
                populateNewOne();
                timeleft = timeleftsave;
            }
        }

        


    }



    





}
