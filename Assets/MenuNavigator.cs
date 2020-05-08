using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour
{


    public int HomeLevel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    private void onHome() {

        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }

    [SerializeField]
    private void onFwd()
    {

        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i + 1);
        Time.timeScale = 1;
    }

    [SerializeField]
    private void onReplay() {

        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i);
        Time.timeScale = 1;

    }


}
