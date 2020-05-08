using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch_universal : MonoBehaviour
{

    [Header("Ustaw wartość w sekundach")]
    public float time;


    public GameObject wincanvas;
    GameObject timeleft;
    public bool mustRun;
    string m;
    string s;
    public GameObject helpCanv;
    public bool isWon;

    public GameObject initCanv;
    public string nazwaLewelaUnikatowa;
    Button resumet;


    // Start is called before the first frame update
    void Start()
    {
        mustRun = true;
        timeleft = GameObject.Find("timeleftTXT");

        wincanvas.SetActive(false);
        isWon = false;

        Time.timeScale = 1;



        if (!PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {

            PlayerPrefs.SetInt(nazwaLewelaUnikatowa, 1);
            Time.timeScale = 0;
            openInitial();
            resumet = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();

        }
    }

    // Update is called once per frame
    void Update()
    {


        if (mustRun) { 
        time -= Time.deltaTime;
    }
        
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        if (minutes < 10)
        {
            m = "0" + minutes.ToString();
        } else
        {
            m =  minutes.ToString();
        }

        if (seconds < 10)
        {
             s = "0" + Mathf.RoundToInt(seconds).ToString();
        } else
        {
            s =  Mathf.RoundToInt(seconds).ToString();
        }


        timeleft.GetComponent<Text>().text = m + ":" + s;

        if (time <0 )
        {
            mustRun = false;
            time = 0;
            
            wincanvas.SetActive(true);
            wincanvas.GetComponent<Animator>().SetBool("canAnimate", true);
            Debug.Log("Wygrana!");
            isWon = true;


           

            Time.timeScale = 0;
            
            
            
        }


        if (InitialHelp.finished == true && PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {

            resumet.onClick.AddListener(closeInitial);
            InitialHelp.finished = false;

        }



    }


    public void openHelp()
    {

        helpCanv.SetActive(true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        SetterPause.CanStart = true;
        Time.timeScale = 0;
        // gameIsActive = false;

        isWon = true;


    }

    public void closeHelp()
    {
        SetterPause.wantToClose = true;
        SetterPause.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        //gameIsActive = true;
        helpCanv.GetComponent<Animator>().SetBool("canHide", true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        helpCanv.SetActive(false);
        isWon = false;
    }



    public void openInitial()
    {
        initCanv.SetActive(true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        InitialHelp.CanStart = true;
        Time.timeScale = 0;
        


       
        isWon = true;
        mustRun = false;

        

        Debug.Log("Opened INITIAL FIRST");
    }


    public void closeInitial()
    {
        InitialHelp.wantToClose = true;
        InitialHelp.message.text = "";
        Time.timeScale = 1;
        // opcjonalne\
        mustRun = true;
        isWon = false;

        initCanv.GetComponent<Animator>().SetBool("canHide", true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        initCanv.SetActive(false);

    }




}
