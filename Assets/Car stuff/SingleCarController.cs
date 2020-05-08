using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCarController : MonoBehaviour
{


    [SerializeField] private GameObject human;
    [SerializeField] private float pollutionIncreaser;
    public float speed = 2.0f;
    public bool isHeld = false;
    public GameObject gameController;
    private GameObject innerHuman;



    private bool direction = false;
    private bool isVisible = false;


  


    void Start()
    {
        if (!direction) transform.Translate(-Vector3.right * speed * Time.deltaTime);
        else transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    public void setDir(bool isRightDir)
    {
        direction = isRightDir;
    }

    public void setSpeed(float sp)
    {
        this.speed = sp;
    }

    public void setController(GameObject gm)
    {
        gameController = gm;
    }


    private void OnMouseDown()
    {

        if (gameController.GetComponent<CarController>().gameIsActive)
        {
            isHeld = true;
            innerHuman = Instantiate(human, transform.position, transform.rotation);
            innerHuman.GetComponent<HumanDragger>().setCar(gameObject);
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "VehicleLeft" || collision.tag == "VehicleRight") 
        {




            if (collision.GetComponent<SingleCarController>().isHeld && gameObject.tag == collision.tag) {
                
                gameController.GetComponent<CarController>().Defeat(true);
            } 

            else if (gameObject.tag == collision.gameObject.tag) collision.GetComponent<SingleCarController>().setSpeed(this.speed);

        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnMouseUp()
    {
        if (gameController.GetComponent<CarController>().gameIsActive)
        {
            innerHuman.GetComponent<HumanDragger>().changeHold();
            isHeld = false;
            Destroy(innerHuman);
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    void Update()
    {
        if (!isHeld) {


            if (!direction) transform.Translate(-Vector3.right * speed * Time.deltaTime); 
            else transform.Translate(Vector3.right * speed * Time.deltaTime); 

        }

        if(!isHeld && isVisible && !gameObject.name.Contains("eco"))
        {
            gameController.GetComponent<CarController>().increasePollution(pollutionIncreaser);
        }


    }
}
