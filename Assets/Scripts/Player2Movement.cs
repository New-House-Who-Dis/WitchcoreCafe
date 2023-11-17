using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal2");
        movement.y = Input.GetAxisRaw("Vertical2");
        if (movement.x == 0 && movement.y == 0) //not moving
        {
            _animator.SetBool("walking", false);
            if (FindObjectOfType<AudioManager>().isPlaying("Footsteps2") == true)
            {
                FindObjectOfType<AudioManager>().Pause("Footsteps2");
            }
        }
        else
        {
            _animator.SetBool("walking", true);
            if (FindObjectOfType<AudioManager>().isPlaying("Footsteps2") == false)
            {
                FindObjectOfType<AudioManager>().Play("Footsteps2");
            }
            if (movement.x == 0)
            {
                if (movement.y == -1) //DOWN: facing down and right
                {
                    movement.x = 1;
                }
                else if (movement.y == 1)
                { //UP: facing up and left
                    movement.x = -1;
                }
            }
            if (movement.y == 0)
            {
                if (movement.x == 1) //RIGHT: facing right and up, FLIP
                {
                    movement.y = 1;
                }
                else if (movement.x == -1) //LEFT: facing left and down, FLIP
                {
                    movement.y = -1;
                }
            }
            if (movement.x != movement.y) //default images are up + left and down + right
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }
        updateDirection(movement.y);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * movement * Time.deltaTime);
    }
    void updateDirection(float y)
    {
        if (y == -1)
        {
            _animator.SetBool("facingDown", true);
        }
        else if (y == 1)
        {
            _animator.SetBool("facingDown", false);
        }
    }
}
