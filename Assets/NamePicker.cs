using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NamePicker : MonoBehaviour
{

    private const string V = "/Resources/TrashToSpawn";
    public Vector3 spawnpoint;
    public GameObject sprite;
    SpriteRenderer sr;
    public String[] arr;


    GameObject gm;
    SpritePicker sp;


    // Start is called before the first frame update
    void Start()
    {
        BetterStreamingAssets.Initialize();

        string[] paths2 = BetterStreamingAssets.GetFiles("trash", "*.png", SearchOption.AllDirectories);
        
        

        /*
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        Debug.Log("Path do tergo to " + Application.streamingAssetsPath);

        FileInfo[] info = dir.GetFiles("*.*");



        
        int counter = 0;
        int amount = 0;



        
        foreach (FileInfo f in info) {
            amount++;
        }

        Debug.Log("Amount to kurwa głyupia0" + amount);

        arr = new String[amount/2];

        foreach (FileInfo f in info)
        {
            Debug.LogWarning("Czy w ogóle foreach kurwa działa");
            
            string nazwa = f.ToString();
            Debug.LogWarning("f.Tostring() = " + nazwa);
            char last = nazwa[nazwa.Length - 1];

            if (nazwa.Contains("meta") == false)
            {

                Debug.LogWarning("Przed Splitowaniem");
                string[] words = nazwa.Split('\\');

                foreach (var word in words)
                {
                    Debug.LogWarning("Przed sprawdzeniem czy zawiera człon PNG");
                    if (word.Contains("png") == true)
                    {
                        string wordexact = word.Replace(".png", string.Empty);

                        arr[counter] = wordexact;
                        counter = counter + 1;
                        Debug.LogWarning("Slowo dodane do listy.");

                    }

                }


            }

        }/// wszystkie nazwy w stringu arrs */





        gm = GameObject.FindGameObjectWithTag("GameController");
        sp = gm.GetComponent<SpritePicker>();



        int r = UnityEngine.Random.Range(0, paths2.Length);
        Debug.Log(r);

        string ok = paths2[r];
        ok = ok.Remove(0, 6);
        ok = ok.Replace(".png", string.Empty);

        Debug.Log(ok);


        this.name = ok;
        var sprite_res = Resources.Load<Sprite>("TrashToSpawn/" + ok);
        this.GetComponent<SpriteRenderer>().sprite = sprite_res;

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnDestroy() {

        if (TrashIdentifier.respawnNext)
        {
            sp.create();
        }
    }
}
