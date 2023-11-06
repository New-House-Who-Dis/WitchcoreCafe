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

    public bool leave = false;
    public bool sitting = false;

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
        transform.position = path[waypointIndex].transform.position;
        Debug.Log(path);
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
