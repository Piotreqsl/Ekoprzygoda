using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameControl : MonoBehaviour
{
    // Start is called before the first frame update



    public GameObject helpCanv;
    public GameObject initialHelp;
    bool helpIsActive = false;
    Button resumet;


    [SerializeField]
    private void openHelp()
    {

        helpCanv.SetActive(true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        SetterPause.CanStart = true;
        Time.timeScale = 0;
        
        //Debug.Log("Opened");

        helpIsActive = true;
        
        LevelUnlocker.HelpIsActive = true;

    }

    [SerializeField]
    private void closeHelp()
    {
        SetterPause.wantToClose = true;
        SetterPause.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        
        helpCanv.GetComponent<Animator>().SetBool("canHide", true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        helpCanv.SetActive(false);
        LevelUnlocker.HelpIsActive = false;

        helpIsActive = false;

    }

    [SerializeField]
    private void openInitial()
    {
        initialHelp.SetActive(true);
        initialHelp.GetComponent<Animator>().SetBool("canAnimate", true);
        InitialHelp.CanStart = true;
        Time.timeScale = 0;
       

        //Debug.Log("Opened");
    }


    public void closeInitial()
    {
        InitialHelp.wantToClose = true;
        InitialHelp.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
        LevelUnlocker.HelpIsActive = false;

        initialHelp.GetComponent<Animator>().SetBool("canHide", true);
        initialHelp.GetComponent<Animator>().SetBool("canAnimate", false);
        initialHelp.SetActive(false);
        
    }


    void Start()
    {
       

        helpCanv.SetActive(false);
        initialHelp.SetActive(false);


        GameObject startText = GameObject.FindGameObjectWithTag("StartText");
        startText.SetActive(false);


        //Player prefs init

       
        

        if (!PlayerPrefs.HasKey("Initial"))
        {
            //Debug.Log("Player prefów nie ma chyba");

            startText.SetActive(true);

            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Sortowanie", 1);//1
            PlayerPrefs.SetInt("Las", 0);//2
            PlayerPrefs.SetInt("Swiatla2", 0);//3
            PlayerPrefs.SetInt("Sortowanie2", 0);//4
            PlayerPrefs.SetInt("Las1", 0);//5
            PlayerPrefs.SetInt("Swiatla", 0);//6
            PlayerPrefs.SetInt("Sortowanie3_2", 0);//7
            PlayerPrefs.SetInt("Swiatla3", 0);//8
            PlayerPrefs.SetInt("Zolw", 0);//9



            openInitial();
            resumet = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();
        }


        else
        {
            Debug.Log("Plasyer prefsy saaaaaaaa");
        }
       
       

    }

    // Update is called once per frame
    void Update()
    {
        

        if ( InitialHelp.finished == true && PlayerPrefs.HasKey("Initial"))
        {
            
            resumet.onClick.AddListener(closeInitial);
            InitialHelp.finished = false;
           

        }
    }

    [SerializeField] private void unlockAll()
    {
        PlayerPrefs.SetInt("Sortowanie", 1);//1
        PlayerPrefs.SetInt("Las", 1);//2
        PlayerPrefs.SetInt("Swiatla2", 1);//3
        PlayerPrefs.SetInt("Sortowanie2", 1);//4
        PlayerPrefs.SetInt("Las1", 1);//5
        PlayerPrefs.SetInt("Swiatla", 1);//6
        PlayerPrefs.SetInt("Sortowanie3_2", 1);//7
        PlayerPrefs.SetInt("Swiatla3", 1);//8
        PlayerPrefs.SetInt("Zolw", 1);//9
    }

}
