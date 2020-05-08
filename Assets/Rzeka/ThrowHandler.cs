using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowHandler : MonoBehaviour
{
    Image throwBar;
    public float bar2 = 1f;

    void Start()
    {
        throwBar = GameObject.Find("throwBar").GetComponent<Image>();
    }

    void Update()
    {
        throwBar.fillAmount = bar2;
    }
}
