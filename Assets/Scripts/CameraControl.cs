using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Character;

    float targetZoom;
    [SerializeField] float zoomFactor;
    [SerializeField] float zoomLerpSpeed;

    private void Start()
    {
        targetZoom = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        //I'ma be honest, I took all of this from a tutorial.
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 2, 20);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

        transform.position = new Vector3(Character.GetComponent<Transform>().position.x, Character.GetComponent<Transform>().position.y, -10);
    }
}
