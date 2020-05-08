using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDetect : MonoBehaviour
{
    public static int trashInNet = 0;
    
    public static bool isFull = false;


    public static void toggleFull()
    {
        isFull = !isFull;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<RiverTrash>() && isFull == false )
        {
            Destroy(other.gameObject);
            trashInNet += 1;

        }
    }
}
