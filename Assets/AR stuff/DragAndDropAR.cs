using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropAR : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float startPosZ;
    public bool isBeingHeld = false;
    private Vector3 mousePos;

    private Vector2 initialPos;
    [SerializeField]private Camera camera;

    private void Start()
    {

        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
      
        
    }


    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld == true)
        {


            Debug.Log("trzyma!");
            mousePos = Input.mousePosition;
            Debug.Log(mousePos);
            //mousePos = camera.ScreenToWorldPoint(mousePos);

            Debug.Log(mousePos);
            this.gameObject.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY + 179, 0);


        }


    }


    public  void Down()
    {
        //&& TrashIdentifier.canDrag
       // if (Input.GetMouseButtonDown(0) )
       // {

            mousePos = Input.mousePosition;
            mousePos = camera.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
        
            isBeingHeld = true;

       // }
    }


    private int collisionCount = 0;



    void OnTriggerEnter2D()
    {
        collisionCount++;
    }

    void OnTriggerExit2D()
    {
        collisionCount--;
    }





    public  void Up()
    {
        isBeingHeld = false;

        
        if (collisionCount == 0)
        {
            this.gameObject.transform.localPosition = new Vector3(0, -179, 0);
        }

        

    }


}
