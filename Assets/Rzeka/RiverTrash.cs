using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RiverTrash : MonoBehaviour
{
    public float speed = 10.0f;
    public float driftSpeed = 200.0f;
    private Rigidbody2D rb;

    private float driftTime;
    private float driftTimeMax;

    ElectricityHandler score;

    private void Awake()
    {
        driftTimeMax = Random.Range(50.0f, 500.0f);
        driftTime = driftTimeMax;
    }

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        score = GameObject.Find("_mainCanvas").GetComponent<ElectricityHandler>();


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -13)
        {
            Destroy(this.gameObject);
            
        }

        ScoreMinus();


    }

    void FixedUpdate()
    {
        RandomDrift(new Vector2(0, Random.Range(-1f, 1f)));
    }


    void ScoreMinus()
    {
        if (transform.position.x < -10)
        {
            score.bar -= 0.0005f;

        }
    }

    public void RandomDrift(Vector2 direction)
    {
        driftTime += Time.deltaTime;
        if (driftTime >= driftTimeMax)
        {
            driftTime -= driftTimeMax;
            
            rb.AddForce(direction * driftSpeed);

        }

    }

}
