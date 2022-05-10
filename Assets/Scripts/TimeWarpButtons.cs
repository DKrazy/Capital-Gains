using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpButtons : MonoBehaviour
{
    public GameObject TimeSystem;

    [SerializeField] int timeWarpSetting;

    public void TimeWarp()
    {
        TimeSystem.GetComponent<TimeSystem>().timeWarpSetting = timeWarpSetting;
    }
}
