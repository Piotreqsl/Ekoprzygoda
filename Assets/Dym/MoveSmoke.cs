using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSmoke : MonoBehaviour
{
    //public gameObject smoke
    public float speed = 0.1f;
    ElectricityHandler eh;


    // Start is called before the first frame update
    void Start()
    {
        eh = GameObject.Find("_mainCanvas").GetComponent<ElectricityHandler>();
        // SerializeField to
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += (transform.right + transform.up) * speed * Time.deltaTime;
        RemoveExpired();
        MinusScore();
    }
    void OnMouseDown()
    {
        Destroy(this.gameObject);
    }

    void RemoveExpired()
    {
        if (transform.position.y >= 7)
        {
            Destroy(this.gameObject);
        }
    }

    void MinusScore()
    {
        if (transform.position.y >= 6)
        {
            eh.bar -= 0.0005f;
        }
    }
}
