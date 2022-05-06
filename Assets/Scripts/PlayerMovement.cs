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
            playerVelocity = new Vector2(playerVelocity.x, movementSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerVelocity = new Vector2(playerVelocity.x, -movementSpeed);
        }
        else
        {
            playerVelocity = new Vector2(playerVelocity.x, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerVelocity = new Vector2(-movementSpeed, playerVelocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerVelocity = new Vector2(movementSpeed, playerVelocity.y);
        }
        else
        {
            playerVelocity = new Vector2(0, playerVelocity.y);
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + playerVelocity * Time.deltaTime);

        Debug.Log(transform.position);
    }
}