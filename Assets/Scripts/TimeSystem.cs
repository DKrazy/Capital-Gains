using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public GameObject Clock;

    float time;
    float minutes;
    float hours;

    string clockMinutes;
    string clockHours;

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= 60 * (hours + 1))
        {
            hours++;
        }

        minutes = Mathf.Floor(time) - (hours * 60);

        if (minutes < 10)
        {
            clockMinutes = "0" + minutes.ToString();
        }
        else
        {
            clockMinutes = minutes.ToString();
        }

        if (hours < 10)
        {
            clockHours = "0" + hours.ToString();
        }
        else
        {
            clockHours = hours.ToString();
        }

        Clock.GetComponent<Text>().text = clockHours + ":" + clockMinutes;
    }
}
