using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelUnlocker : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("Wygrana pokazala sie!");

        if (PlayerPrefs.HasKey(nextLevel))
        {
            if (PlayerPrefs.GetInt(nextLevel) != 1)
            {
                PlayerPrefs.SetInt(nextLevel, 1);
            }
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
