using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject TimeSystem;

    [SerializeField] float movementSpeed;

    int timeWarpSetting;

    public Vector2 playerSpeed;

    public Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeWarpSetting = TimeSystem.GetComponent<TimeSystem>().timeWarpSetting;

        if (Input.GetKey(KeyCode.W))
        {
            playerSpeed = new Vector2(playerSpeed.x, movementSpeed * timeWarpSetting);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerSpeed = new Vector2(playerSpeed.x, -movementSpeed * timeWarpSetting);
        }
        else
        {
            playerSpeed = new Vector2(playerSpeed.x, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerSpeed = new Vector2(-movementSpeed * timeWarpSetting, playerSpeed.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerSpeed = new Vector2(movementSpeed * timeWarpSetting, playerSpeed.y);
        }
        else
        {
            playerSpeed = new Vector2(0, playerSpeed.y);
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + playerSpeed * Time.deltaTime);
    }
}