using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField]
    private string horizontalInputName;
    [SerializeField]
    private string verticalInputName;

    enum Direction { Left, Right, Up, Down };
    public float moveSpeed = 2.5f;
    public Rigidbody2D rb;
    Vector2 movement;

    [SerializeField]
    private Vector3 drinkRightOffset;
    [SerializeField]
    private Vector3 drinkLeftOffset;
    private GameObject drinkGameObject;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        drinkGameObject = transform.Find("Drink").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //freezes input when player is "busy" (i.e cooking)
        movement.x = _animator.GetBool("cooking") ? 0 : Input.GetAxisRaw(horizontalInputName);
        movement.y = _animator.GetBool("cooking") ? 0: Input.GetAxisRaw(verticalInputName);
        

        if (movement.x == 0 && movement.y == 0) //not moving
        {
            _animator.SetBool("walking", false);
            if (FindObjectOfType<AudioManager>().isPlaying("Footsteps1") == true)
            {
                FindObjectOfType<AudioManager>().Pause("Footsteps1");
            }
        }
        else
        {
            _animator.SetBool("walking", true);
            if (FindObjectOfType<AudioManager>().isPlaying("Footsteps1") == false)
            {
                FindObjectOfType<AudioManager>().Play("Footsteps1");
            }
            if (movement.x == 0)
            {
                if (movement.y == -1) //DOWN: facing down and right
                {
                    movement.x = 1;
                    drinkGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    drinkGameObject.transform.localPosition = drinkRightOffset;
                }
                else if (movement.y == 1)
                { //UP: facing up and left
                    movement.x = -1;
                    drinkGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    drinkGameObject.transform.localPosition = drinkLeftOffset;
                }
            }
            if (movement.y == 0)
            {
                if (movement.x == 1) //RIGHT: facing right and up, FLIP
                {
                    movement.y = 1;
                    drinkGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    drinkGameObject.transform.localPosition = drinkRightOffset;
                }
                else if (movement.x == -1) //LEFT: facing left and down, FLIP
                {
                    movement.y = -1;
                    drinkGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    drinkGameObject.transform.localPosition = drinkLeftOffset;
                }
            }

            if (movement.x != movement.y) //default images are up + left and down + right
            {
                _spriteRenderer.flipX = false;
            } else {
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
        } else if (y == 1)
        {
            _animator.SetBool("facingDown", false);
        }
    }

    public void updateBusy(bool busy)
    {
        _animator.SetBool("cooking", busy); //plays or stops cooking animation based on bool
    }
}
