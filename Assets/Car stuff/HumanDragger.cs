using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDragger : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = true;
    private Vector3 mousePos;

    public GameObject car;
   
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

 
    }



    public void changeHold()
    {
        isBeingHeld = false;
    }

    public void setCar(GameObject cars)
    {
        car = cars;
    }


    // Update is called once per frame
    void Update()
    {


            //Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = camera.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

    }

    







}
