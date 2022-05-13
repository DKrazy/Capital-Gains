using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuroClockToggle : MonoBehaviour
{
    public GameObject TimeSystem;

    private void Update()
    {
        TimeSystem.GetComponent<TimeSystem>().euroClock = GetComponent<Toggle>().isOn;
    }
}
