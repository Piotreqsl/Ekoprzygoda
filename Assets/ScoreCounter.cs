using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public static int scoreVal = 0;
    public int maxScore;


    Text score;


    // Start is called before the first frame update
    void Start()
    {
        scoreVal = 0;
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Pozostało: " +(maxScore - scoreVal); 
    }
}
