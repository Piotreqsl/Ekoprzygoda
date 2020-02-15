using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = System.Random;

public class ForestPopulator : MonoBehaviour
{
    private const string V = "/Assety/Resources/TrashToSpawn";
    public GameObject sprite;

    SpriteRenderer sr;
    public String[] arr;
    public int numberOfPositions;
   

    public int timeToRespawnNewOne;



    GameObject[] goList;
    public GameObject[] vectorki;
    public static GameObject[] vectorki2;
    public static int dlugosc;
    public static int[] used;

    float timeleft;
    float timeleftsave;

    public static GameObject spriteSecond;
    GameObject errorcanvas;

    public static int numberOfItems;// Liczba itemków na planszy
    int counterPomocniczy = 1;// do funkcji na jednego

    public static Boolean gameIsActive = true;


    public void create(int number)
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

            GameObject test = vectorki[rand];
            Vector3 wektor = test.transform.position;

            


            sprite.gameObject.tag = "trash";
            Instantiate(sprite);
            sprite.transform.position = wektor;

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





    // Start is called before the first frame update
    void Start()
    {
        // Array na ilosc respawnów i sprite'y
        vectorki = GameObject.FindGameObjectsWithTag("Respawn");
        vectorki2 = vectorki;
        dlugosc = vectorki.Length;
        spriteSecond = sprite;

       
        // Miejsca w użyciu
        used = new int[dlugosc + 1];




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

        errorcanvas = GameObject.FindGameObjectWithTag("ErrorCanvas");
        errorcanvas.GetComponent<Canvas>().enabled = false;






        /// Ustawianie czasu
        timeleft = (float)timeToRespawnNewOne;
        timeleftsave = (float)timeToRespawnNewOne;





    }






    public void Victory()
    {
        Debug.Log("Zwycięstwo!");
    }




    // Update is called once per frame
    void Update()
    {
       

        if (numberOfItems == 0)
        {
            Victory();
            Destroy(gameObject);
        }



        /// Respienie nowego itemka
          timeleft = timeleft - Time.deltaTime;
           if (timeleft < 0)
        {
            populateNewOne();
            timeleft = timeleftsave;
        }


        


    }
}
