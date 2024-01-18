using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] path;
    public float moveSpeed = 1f;
    private int waypointIndex = 0;
    private int prevwaypointIndex = -1;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public GameObject alertPrefab;
    private Animator alertAnimator;

    public bool leave = false;
    public bool sitting = false; //TODO: consider removing sitting bools and instead retrieving it from the animator parameter isWalking 

    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leave)
        {
            leaveTable();
        } else if (!leave && !sitting)
        {
            moveToTable();
        }
    }

    public void setPath(Transform[] newPath)
    { 
        path = newPath;
        Debug.Log("hi1");
        transform.position = path[waypointIndex].transform.position;
        Debug.Log("hi2");
    }
    
    public bool movingLeft(Transform currentTransform)
    {
        if (prevwaypointIndex != -1)
        {
            if (currentTransform.position.x < path[prevwaypointIndex].transform.position.x)
            {
                return true;
            }
        }
        return false;
    }

    public bool moveToTable()
    {
        if (waypointIndex < path.Length)
        {
            _animator.SetBool("isWalking", true);
            //Moving the NPC towards the location
            transform.position = Vector2.MoveTowards(transform.position, path[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            if (movingLeft(transform))
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
            if (Vector2.Distance(transform.position, path[waypointIndex].transform.position) < 0.2)
            {
                prevwaypointIndex = waypointIndex;
                waypointIndex++;
            }
            return false;
        } else
        {
            // Instantiate "ready to order" VFX
            GameObject newAlert = Instantiate(alertPrefab, transform.position + new Vector3(0.5f, 1f, 0f), Quaternion.identity);
            newAlert.transform.parent = transform;
            newAlert.name = "Alert";
            alertAnimator = newAlert.GetComponent<Animator>();

            _animator.SetBool("isWalking", false);
            sitting = true; 
            prevwaypointIndex = path.Length - 1;
            waypointIndex = path.Length - 1;
        }
        return true;
    }

    public bool leaveTable()
    {
        if (waypointIndex >= 0)
        {
            if (transform.Find("Alert") != null) {
                // First time calling leaveTable()
                Destroy(transform.Find("Alert").gameObject);
                alertAnimator = null;
                _animator.SetBool("isWalking", true);
            }

            //Moving the NPC towards the location
            transform.position = Vector2.MoveTowards(transform.position, path[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            if (movingLeft(transform))
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
            if (Vector2.Distance(transform.position, path[waypointIndex].transform.position) < 0.2)
            {
                prevwaypointIndex = waypointIndex;
                waypointIndex--;
            }
        }
        else
        {
            Destroy(gameObject); //delete self
            return true;
        }
        return false;
    }
}
