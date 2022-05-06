using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    public Vector2 playerSpeed;

    public Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerSpeed = new Vector2(playerSpeed.x, movementSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerSpeed = new Vector2(playerSpeed.x, -movementSpeed);
        }
        else
        {
            playerSpeed = new Vector2(playerSpeed.x, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerSpeed = new Vector2(-movementSpeed, playerSpeed.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerSpeed = new Vector2(movementSpeed, playerSpeed.y);
        }
        else
        {
            playerSpeed = new Vector2(0, playerSpeed.y);
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + playerSpeed * Time.deltaTime);

        Debug.Log(transform.position);
    }
}