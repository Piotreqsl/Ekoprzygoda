using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlocker : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string LevelName;
    [SerializeField] private bool canClick = false;
    public static bool HelpIsActive = false;
    

    void Start()
    {

        Color col = new Color(0.7830189f, 0.7830189f, 0.7830189f, 0.8f);

        if (PlayerPrefs.HasKey(LevelName))
        {
            if (PlayerPrefs.GetInt(LevelName) == 1)
            {

                canClick = true; 

            }
        }


        if(!canClick)
        {
            this.GetComponent<SpriteRenderer>().color = col;
        }

        if (canClick) {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {

        Debug.Log("click" + PlayerPrefs.GetInt(LevelName));

        if (canClick && HelpIsActive == false)
        {
            SceneManager.LoadScene(LevelName);
        }



    }
}
