using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionModeToggle : MonoBehaviour
{
    public GameObject ConstructionManager;

    private void Update()
    {
        ConstructionManager.GetComponent<ConstructionManager>().constructionMode = GetComponent<Toggle>().isOn;
    }
}
