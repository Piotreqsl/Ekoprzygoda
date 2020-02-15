using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = false;
    private Vector3 mousePos;

    private Vector2 initialPos;
     private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        
        initialPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld == true) {

            //Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = camera.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
           

        }


    }

    private void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0) && TrashIdentifier.canDrag)
        {
           
            mousePos = Input.mousePosition;
            mousePos = camera.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;

        }
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




    private void OnMouseUp()
    {
        isBeingHeld = false;


        if (collisionCount == 0)
        {
        this.gameObject.transform.localPosition = new Vector3(initialPos.x, initialPos.y, 0);
        }

    }


}
