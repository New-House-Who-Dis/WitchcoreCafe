using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal2");
        movement.y = Input.GetAxisRaw("Vertical2");

        if (movement.x == 0)
        {
            if (movement.y == -1)
            {
                movement.x = 1;
            }
            else if (movement.y == 1)
            {
                movement.x = -1;
            }
        }
        if (movement.y == 0)
        {
            if (movement.x == 1)
            {
                movement.y = 1;
            }
            else if (movement.x == -1)
            {
                movement.y = -1;
            }
        }
        Debug.Log("WHEEE");
        Debug.Log(movement);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * movement * Time.deltaTime);
    }
}
