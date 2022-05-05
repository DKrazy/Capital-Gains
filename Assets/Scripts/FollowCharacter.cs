using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public GameObject Character;

    private void Update()
    {
        transform.position = new Vector3(Character.GetComponent<Transform>().position.x, Character.GetComponent<Transform>().position.y, -10);
    }
}
