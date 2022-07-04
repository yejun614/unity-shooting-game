using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopWatch : MonoBehaviour
{
    public float Timestamp = 0;
    public bool IsActive = true;

    TMP_Text clock;

    void Awake()
    {
        clock = GetComponent<TMP_Text>();
    }

    void FixedUpdate()
    {
        if (IsActive) Timestamp += Time.deltaTime;
        
        int minutes = (int)(Timestamp / 60);
        int seconds = (int)(Timestamp % 60);

        clock.text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
    }
}
