using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score;
    public int maxScore;
    public Text scoreDisplay;
    public GameObject wincanvas;
    public GameObject gameovercanvas;


    private void Start()
    {
        wincanvas.SetActive(false);
        gameovercanvas.SetActive(false);
    }

    private void Update()
    {
        scoreDisplay.text = score.ToString()+"/"+maxScore.ToString();

        if (score >= maxScore)
        {
            Time.timeScale = 0;
            wincanvas.SetActive(true);
            wincanvas.GetComponent<Animator>().SetBool("canAnimate", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        score++;
        Destroy(other.gameObject);
    }
}
