using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessTimer : MonoBehaviour
{
    public float time = 0f;
    private float mins;
    private float secs;
    public Text timeText;


    //creates a minutes and seconds timer for game
    void FixedUpdate()
    {
        time += Time.deltaTime;
        Display();
    }

    //displays time in minutes and seconds
    public void Display()
    {
        mins = Mathf.FloorToInt(time / 60);
        secs = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
}
