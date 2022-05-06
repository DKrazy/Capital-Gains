using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Character;
    public float targetZoom;
    public float zoomFactor = 3;
    public float zoomLerpSpeed;

    private void Start()
    {
        targetZoom = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 2, 20);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

        transform.position = new Vector3(Character.GetComponent<Transform>().position.x, Character.GetComponent<Transform>().position.y, -10);
    }
}
