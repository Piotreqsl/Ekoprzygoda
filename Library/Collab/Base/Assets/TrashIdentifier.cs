using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashIdentifier : MonoBehaviour
{

    BoxCollider2D bc;
    public string rodzaj;
    SpriteRenderer sr;
    public string[] acceptedItems;
    public Vector3 spawnpoint;
    public SpritePicker s;

    


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();

     

    }

    void OnTriggerEnter2D(Collider2D b)
    {



        var kosz = Resources.Load<Sprite>("trashcans/" + rodzaj + "_open");
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = kosz;



    }

    void OnTriggerExit2D(Collider2D b1)
    {
        var kosz = Resources.Load<Sprite>("trashcans/" + rodzaj + "_closed");
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = kosz;
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;

        if (b1.transform.position == spawnpoint)
        {


            int pos = Array.IndexOf(acceptedItems, b1.name);

            if (pos > -1)
            {
                Debug.Log("Poprawnie wyrzucony " + b1.name + " do " + rodzaj);
                GameObject trashCorrect = GameObject.FindGameObjectWithTag("trash");
                Destroy(trashCorrect);

                



            }
            else
            {
                Debug.Log("Niepoprawnie wyrzucony " + b1.name + " do " + rodzaj);
            }


        }




    }

    // Update is called once per frame
    void Update()
    {



    }
}
