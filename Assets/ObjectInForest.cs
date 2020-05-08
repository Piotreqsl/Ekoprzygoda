using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInForest : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private float delay = 0f;










    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        anim.enabled = false;





    }

    // Update is called once per frame
    void Update()
    { 

        if (ForestPopulator.gameIsActive == false && ForestPopulator.hasDefeated == true)
        {
            Destroy(gameObject);
        }

        


        
        
        
    }


 


   
   

   


    private void OnMouseDown()
    {

        if (ForestPopulator.gameIsActive == true)
        {
            transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
            anim.enabled = true;
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
            ForestPopulator.numberOfItems--;
        }



    }


}
