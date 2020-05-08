using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpritePicker : MonoBehaviour
{
   
    [SerializeField] private Vector3 spawnpoint;
    public GameObject sprite;
    SpriteRenderer sr;
    [SerializeField] private String[] arr;

    public GameObject helpCanv;



    public void create()
    {
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        sprite.gameObject.tag = "trash";
        Instantiate(sprite);
        sprite.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        sprite.transform.position = spawnpoint;
    }


    public void openHelp()
    {

        helpCanv.SetActive(true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", true);
        SetterPause.CanStart = true;
        Time.timeScale = 0;
        TrashIdentifier.canDrag = false;
        
        Debug.Log("Opened");


    }

    public void closeHelp()
    {
        SetterPause.wantToClose = true;
        SetterPause.message.text = "";
        Time.timeScale = 1;
        TrashIdentifier.canDrag = true;
        helpCanv.GetComponent<Animator>().SetBool("canHide", true);
        helpCanv.GetComponent<Animator>().SetBool("canAnimate", false);
        helpCanv.SetActive(false);

    }







    // Start is called before the first frame update
    void Start()
    {

        try {
            Destroy(GameObject.FindGameObjectWithTag("trash"));
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        create();
        


    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
