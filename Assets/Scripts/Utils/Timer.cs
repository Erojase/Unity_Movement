using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private bool is_runner = true;

    private GameObject timer;

    private int minute = 0;
    private int second = 0;
    private float millisecond = 0F;

    // Start is called before the first frame update
    void Start()
    {
        if (is_runner)
        {
            timer = GameObject.Find("Tiempo");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (is_runner)
        {
            millisecond += 1 * (Time.deltaTime * 100);
            if (millisecond > 99)
            {
                millisecond = 0;
                second++;
            }
            if (second > 59)
            {
                second = 0;
                minute++;
            }
            UpdateTimer();
        }
    }

    public void UpdateTimer()
    {
        Text text = timer.GetComponent<Text>();
        string strSec = "";
        string strMin = "";
        if (second < 10)
        {
            strSec = "0" + second;
        }
        else
        {
            strSec = "" + second;
        }
        if (minute < 10)
        {
            strMin = "0" + minute;
        }
        else
        {
            strMin = "" + minute;
        }
        text.text = strMin + ":" + strSec + ":" + millisecond;

        if (transform.position.y < -4)
        {
            restartTimer();
        }
    }

    public void restartTimer()
    {
        minute = 0;
        second = 0;
        millisecond= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "finish")
        {
            is_runner = false;
        }
    }
}
