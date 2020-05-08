using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NamePicker : MonoBehaviour
{

    private const string V = "/Resources/TrashToSpawn";
    [SerializeField] private Vector3 spawnpoint;
    [SerializeField] private GameObject sprite;
    SpriteRenderer sr;
    [SerializeField] private String[] arr;


    GameObject gm;
    SpritePicker sp;


    // Start is called before the first frame update
    void Start()
    {
        BetterStreamingAssets.Initialize();

        string[] paths2 = BetterStreamingAssets.GetFiles("new_trash", "*.png", SearchOption.AllDirectories);
        
        

        /*
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        Debug.Log("Path do tergo to " + Application.streamingAssetsPath);

        FileInfo[] info = dir.GetFiles("*.*");



        
        int counter = 0;
        int amount = 0;



        
        foreach (FileInfo f in info) {
            amount++;
        }

      

        arr = new String[amount/2];

        foreach (FileInfo f in info)
        {
            
            
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
        ok = ok.Remove(0, 10);
        ok = ok.Replace(".png", string.Empty);

        Debug.Log(ok);


        this.name = ok;
        var sprite_res = Resources.Load<Sprite>("NewTrashToSpawn/" + ok);
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
