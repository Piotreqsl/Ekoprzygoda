using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpcanvasdisplayer : MonoBehaviour
{


    public GameObject helpCanv;
    public GameObject initCanv;

    public string nazwaLewelaUnikatowa;
    Button resumet;
 public void openHelp()
    {

        helpCanv.SetActive(true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        SetterPause.CanStart = true;
        Time.timeScale = 0;
       // gameIsActive = false;
       // Debug.Log("Opened");


    }

    public void closeHelp()
    {
        SetterPause.wantToClose = true;
        SetterPause.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
      // gameIsActive = true;
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

      

        Debug.Log("Opened");
    }


    public void closeInitial()
    {
        InitialHelp.wantToClose = true;
        InitialHelp.message.text = "";
        Time.timeScale = 1;
        // opcjonalne
       

        initCanv.GetComponent<Animator>().SetBool("canHide", true);
        initCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        initCanv.SetActive(false);

    }



    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(nazwaLewelaUnikatowa))
        {
            PlayerPrefs.SetInt(nazwaLewelaUnikatowa, 1);
            openInitial();
            resumet = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();

        }
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
