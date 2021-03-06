﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetterPause : MonoBehaviour, IPointerClickHandler
{

    //public Sprite guideRes;

    [SerializeField] private Sprite textBackground;
    Image TextBG;

    Image img;
    [SerializeField] private string wiadomosc;
    public static Text message;
    // public float delay;

    //public static float delayother;

    [SerializeField] private float delayStart;

    public static float delayotherStart;


    public static string wiadomoscother;

    public static bool CanStart = false;
    [SerializeField] private static bool isPlaying = false;



    public static bool wantToClose = false;






    public static IEnumerator c1;

    [SerializeField] private static Coroutine theCoroutine;


    [SerializeField] private AudioSource VoiceSource;



    [SerializeField] private AudioClip guideClip;
    [SerializeField] private bool hasMoreThanOneAudioClip;



    float tekst;
    float audio;

    [SerializeField] private static float interval;








    // Start is called before the first frame update


    void Start()
    {





        message = GameObject.FindGameObjectWithTag("GuideText").GetComponent<Text>();
        message.text = "";

        TextBG = GameObject.FindGameObjectWithTag("GuideTextBG").GetComponent<Image>();
        TextBG.sprite = textBackground;

        VoiceSource.clip = guideClip;












        //delayother = delay;
        wiadomoscother = wiadomosc;
        delayotherStart = delayStart;


        audio = VoiceSource.clip.length;
        tekst = wiadomosc.Length;

        interval = countInterval(audio, tekst);

       // Debug.Log("Dlugosc audio  to " + audio);
        //Debug.Log("Dlugosc tekst  to " + tekst);
        //Debug.Log("Dlugosc  interva to " + interval);


    }



    float countInterval(float clipl, float tekstl)
    {
        float wynik = (clipl - delayotherStart) / (tekst - 1);
        return wynik;
    }



    [SerializeField]
    private static IEnumerator PlayText()
    {
        yield return new WaitForSecondsRealtime(delayotherStart);
        

        foreach (char c in wiadomoscother)
        {
            message.text += c;
            yield return new WaitForSecondsRealtime(interval);


        }


        CanStart = false;
    }





    void open()
    {
        theCoroutine = StartCoroutine(PlayText());
        isPlaying = true;
        VoiceSource.PlayDelayed(delayotherStart);
        //Debug.Log("Zaczelo grac");

    }


    void close()
    {
        //Debug.Log("Zastopowało");
        StopAllCoroutines();
        isPlaying = false;
        message.text = "";
        VoiceSource.Stop();

    }






    // Update is called once per frame
    void Update()
    {


        if (wantToClose == true)
        {

            close();


            wantToClose = false;
        }





        if (CanStart == true)
        {
            open();
            CanStart = false;

        }


       




    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Nacisk na canvasa;");

        StopAllCoroutines();
        message.text = wiadomosc;
    }
}
