using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePickerAR : MonoBehaviour
{
    
    GameObject GameController;
    SpritePickerAR sp;


    // Start is called before the first frame update
    void Start()
    {
        BetterStreamingAssets.Initialize();

        string[] paths2 = BetterStreamingAssets.GetFiles("new_trash", "*.png", System.IO.SearchOption.AllDirectories);
        GameController = GameObject.FindGameObjectWithTag("GameController");
        sp = GameController.GetComponent<SpritePickerAR>();



        int r = UnityEngine.Random.Range(0, paths2.Length);
        Debug.Log(r);

        string ok = paths2[r];
        ok = ok.Remove(0, 10);
        ok = ok.Replace(".png", string.Empty);

        Debug.Log(ok);


        this.name = ok;
        var sprite_res = Resources.Load<Sprite>("NewTrashToSpawn/" + ok);
        this.GetComponent<Image>().sprite = sprite_res;

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnDestroy()
    {

       // if (TrashIdentifier.respawnNext)
        //{
            sp.create();
        //}
    }
}
