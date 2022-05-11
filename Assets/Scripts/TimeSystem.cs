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

    [SerializeField] bool euroClock;

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

        //I think if the entire world used 24 hour clocks, programmers wouldn't have to deal with all this shit.
        if (euroClock)
        {
            clockHours = hours.ToString();

            Clock.GetComponent<Text>().text = clockHours + ":" + clockMinutes;
        }

        //This implementation of a 12 hour clock is a complete mess, but it works and that's all that matters.
        if (!euroClock)
        {
            if (hours == 0 || hours == 12)
            {
                clockHours = "12";
            }
            else if (hours > 0 && hours <= 12)
            {
                clockHours = hours.ToString();
            }

            if (hours < 12)
            {
                Clock.GetComponent<Text>().text = clockHours + ":" + clockMinutes + " AM";
            }
            else if (hours > 12)
            {
                clockHours = (hours - 12).ToString();
            }

            if (hours >= 12)
            {
                Clock.GetComponent<Text>().text = clockHours + ":" + clockMinutes + " PM";
            }
        }

        if (time >= 1440)
        {
            time = 0;

            minutes = 0;
            hours = 0;
        }
    }
}
