using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuroClockToggle : MonoBehaviour
{
    public GameObject TimeSystem;

    private void Update()
    {
        if (GetComponent<Toggle>().isOn)
        {
            TimeSystem.GetComponent<TimeSystem>().euroClock = true;
        }
        else
        {
            TimeSystem.GetComponent<TimeSystem>().euroClock = false;
        }
    }
}
