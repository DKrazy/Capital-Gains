using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenuButtons : MonoBehaviour
{
    public GameObject ConstructionManager;

    [SerializeField] int id;

    public void ChangeSelectedID()
    {
        if (ConstructionManager.GetComponent<ConstructionManager>().constructionMode)
        {
            ConstructionManager.GetComponent<ConstructionManager>().selectedID = id;
        }
    }
}
