using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyerCycleZone : MonoBehaviour
{
    [SerializeField] private GameObject CycleSpawnpoint;
    [SerializeField] private GameObject CyclePrefab;
    


    void Start()
    {
        
    }

    

    void Update()
    {
        
    }


   


    private void OnTriggerExit2D(Collider2D collision)
    {


        if (!collision.GetComponent<HumanDragger>().car.GetComponent<SingleCarController>().isHeld && collision.tag == "Human") // Jeśli to człowiek
        {
            // Jeśli samochod jest eco
            if (collision.GetComponent<HumanDragger>().car.name.Contains("eco")) collision.GetComponent<HumanDragger>().car.GetComponent<SingleCarController>().gameController.GetComponent<CarController>().addTime();

          

            // Animacja rozwałki i rozwałka 
            collision.GetComponent<HumanDragger>().car.GetComponent<SingleCarController>().setSpeed(0f);
            collision.GetComponent<HumanDragger>().car.GetComponent<Animator>().Play("Puff");
            Destroy(collision.GetComponent<HumanDragger>().car, collision.GetComponent<HumanDragger>().car.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.05f );

            Instantiate(CyclePrefab, CycleSpawnpoint.transform.position, CycleSpawnpoint.transform.rotation);
       }
    }



}
