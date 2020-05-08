using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour
{
    private enum State
    {
        Rotation,
        ThrowNetPower,
        DetectForce,
        RecallNet,
        ResetPosition
    }

    private State state;
    
    public float speed = 2f;
    public float throwSpeed = 0.05f;
    public float recallSpeed = 2f;
    public float maxRotation = 35f;
    private float returnRotationSpeed = 0.2f;

    public float force;
    private bool powerDir = true;

    ThrowHandler powerThrow;
    public GameObject ThrowNetCanvas;


    void Start()
    {
        powerThrow = GameObject.Find("_throwNetCanvas").GetComponent<ThrowHandler>();
        ThrowNetCanvas.SetActive(false);
    }

    void Update()
    {
        switch (state)
        {
            case State.Rotation:
                Rotation();
                StopRotation();
                break;
            case State.ThrowNetPower:
                ActivateThrowNetCanvas();
                PowerHandler();
                if (Input.GetMouseButtonDown(0))
                {
                    GetBarPower();
                }
                break;
            case State.DetectForce:
                StartCoroutine(NetThrow());
                break;
            case State.RecallNet:
                RecallNet();
                DetectResetPosition();
                DetectItemsInNet();
                break;
            case State.ResetPosition:
                TrashDetect.toggleFull();
                TrashDetect.trashInNet = 0;
                ResetNetPosition();
                break;
        }
    }
    
    //ThrowNetPowerBar

    void ActivateThrowNetCanvas()
    {
        ThrowNetCanvas.SetActive(true);
    }

    void PowerHandler()
    { 
        if (powerThrow.bar2 >= 1)
        {
            powerDir = false;
        }
        else if (powerThrow.bar2 <= 0)
        {
            powerDir = true;
        }

        if (powerDir)
        {
            powerThrow.bar2 += 0.03f;
        }
        else
        {
            powerThrow.bar2 -= 0.03f;
        }
        
    }

    public float GetBarPower()
    {
        force = powerThrow.bar2;
        state = State.DetectForce;
        return force;
    }


    //Player

    void Rotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
    }

    void StopRotation()
    {
        if (Input.GetMouseButtonDown(0)) {
            state = State.ThrowNetPower;

        }
    }
    void NetThrowing(float force)
    {
        transform.Translate(transform.up * force * throwSpeed * Time.smoothDeltaTime);
    }
     
    void RecallNet()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(transform.up * -recallSpeed * Time.smoothDeltaTime);
        }
        
    }



    void DetectResetPosition()
    {
        if (transform.position.y <= -3.7)
        {
            state = State.ResetPosition;
        }

    }

    void DetectItemsInNet()
    {
        if(TrashDetect.trashInNet >= 4)
        {
            TrashDetect.isFull = true;
           
        }
    }

    void ResetNetPosition()
    {
        if (transform.rotation.eulerAngles.z >= 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -35f * returnRotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 35f * returnRotationSpeed * Time.deltaTime);
        }
        state = State.Rotation;
        
    }


    IEnumerator NetThrow()
    {
        // This will wait 1 second like Invoke could do, remove this if you don't need it
        yield return new WaitForSeconds(1);


        float timePassed = 0;
        while (timePassed < 1)
        {
            NetThrowing(force);
            timePassed += Time.deltaTime;

            yield return null;
        }
        state = State.RecallNet;
    }

}
