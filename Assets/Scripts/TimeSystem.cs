using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public GameObject Clock;

    public int timeWarpSetting = 1;

    float time;
    float minutes;
    float hours;
    float timeWarp;

    string clockMinutes;
    string clockHours;

    private void Update()
    {
        timeWarp = timeWarpSetting * Time.deltaTime;

        time += timeWarp;

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
