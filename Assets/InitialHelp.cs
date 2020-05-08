using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitialHelp : MonoBehaviour, IPointerClickHandler
{
    public Sprite textBackground;
    Image TextBG;

    Image img;
    public string wiadomosc;
    public static string wiadomoscother;



    public string wiadomosc2;
    public static string wiadomoscother2;
    public static Text message;
    // public float delay;

    //public static float delayother;

    public float delayStart;

    public static float delayotherStart;





    public static bool CanStart = false;
    public static bool isPlaying = false;



    public static bool wantToClose = false;


    public static bool finished = false;






    public static Coroutine theCoroutine;
    public static Coroutine theCoroutine2;


    public AudioSource VoiceSource;



    public AudioClip guideClip;
    public AudioClip guideClip2;
    public bool hasMoreThanOneAudioClip;



    float tekst;
    float audio;

    bool isfirst ;
    bool issecond;

    public static float interval;








    // Start is called before the first frame update


    void Start()
    {





        message = GameObject.FindGameObjectWithTag("GuideText").GetComponent<Text>();
        message.text = "";

        TextBG = GameObject.FindGameObjectWithTag("GuideTextBG").GetComponent<Image>();
        TextBG.sprite = textBackground;

        VoiceSource.clip = guideClip;




        PlayerPrefs.SetInt("Initial", 1);






        //delayother = delay;
        wiadomoscother = wiadomosc;
        wiadomoscother2 = wiadomosc2;




        delayotherStart = delayStart;


        audio = VoiceSource.clip.length;
        tekst = wiadomosc.Length;

        interval = countInterval(audio, tekst);



    }



     float countInterval(float clipl, float tekstl)
    {
        float wynik = (clipl - delayotherStart) / (tekst - 1);
        return wynik;
    }



    public static IEnumerator PlayText(string tekst, float dlugoscclipa, float delays)
    {
        yield return new WaitForSecondsRealtime(delays);

        float interv = (dlugoscclipa - delays) / (tekst.Length - 1);


        foreach (char c in tekst)
        {
            message.text += c;
            yield return new WaitForSecondsRealtime(interv);


        }


        CanStart = false;
    }





    void open()
    {


        float dlugoscclipa = VoiceSource.clip.length;
        theCoroutine = StartCoroutine(PlayText(wiadomoscother, dlugoscclipa, delayotherStart));
        isPlaying = true;
        VoiceSource.PlayDelayed(delayotherStart);
        Debug.Log("Zaczelo grac");

        Button resume = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();
        resume.onClick.AddListener(skip1);
        isfirst = true;  


    }


    void close()
    {
        //Debug.Log("Zastopowało");
        StopAllCoroutines();
        isPlaying = false;
        message.text = "";
        VoiceSource.Stop();

    }


    void skip1()
    {
        StopAllCoroutines();
        message.text = wiadomosc;
    }

  


    void open2()
    {
        message.text = "";
        isfirst = false;
        issecond = true;


        Button resume = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();
        resume.onClick.RemoveAllListeners();
        VoiceSource.Stop();

        VoiceSource.clip = guideClip2;
        
        float dlugoscclipa = VoiceSource.clip.length;
        
        theCoroutine2 = StartCoroutine(PlayText(wiadomoscother2, dlugoscclipa, 0));

        VoiceSource.Play();

        resume.onClick.AddListener(skip2);


    }

    void skip2()
    {
        StopAllCoroutines();
        message.text = wiadomoscother2;

        Button resume = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();
        resume.onClick.RemoveAllListeners();
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



        if (message.text == wiadomoscother)
        {

            StopAllCoroutines();
            Button resume = GameObject.FindGameObjectWithTag("InitialResume").GetComponent<Button>();
            resume.onClick.RemoveAllListeners();
            resume.onClick.AddListener(open2);  
            
        }


        if (message.text == wiadomoscother2)
        {
            finished = true;
        }






    }

   public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Nacisk na canvasa;");

        StopAllCoroutines();

        if (isfirst)
        {
            message.text = wiadomosc;
        }
        if (issecond)
        {
            message.text = wiadomoscother2;
        }

        }


}


