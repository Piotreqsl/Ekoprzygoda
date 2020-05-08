using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCenterScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 pos;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pos;
        this.gameObject.transform.localScale = new Vector3(1, 1, 0);
    }
}
