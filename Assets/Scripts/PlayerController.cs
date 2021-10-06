using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private int speed = 12;
    private Vector3 targetPosition;
    private ContactFilter2D layerFilter;
    public int lastDirection;

    void Start()
    {
        targetPosition = transform.position;
        layerFilter.SetLayerMask(1 << 10);
        layerFilter.SetLayerMask(1 << 9);
        lastDirection = 4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //MovePlayer();
    }

    public GameObject GetObjectInFront()
    {
        RaycastHit2D hitObject;
        GameObject objectInFront = null;

        try
        {
            switch (lastDirection)
            {
                case 1:
                    hitObject = Physics2D.Raycast(transform.position, Vector2.left, 1);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                case 2:
                    hitObject = Physics2D.Raycast(transform.position, Vector2.right, 1);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                case 3:
                    hitObject = Physics2D.Raycast(transform.position, Vector2.up, 1);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                case 4:
                    hitObject = Physics2D.Raycast(transform.position, Vector2.down, 1);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                default:
                    objectInFront = null;
                    break;
            }
        }
        catch (System.NullReferenceException e)
        {
            objectInFront = null;
        }

        //if (objectInFront != null)
        //    Debug.Log(objectInFront.name);

        return objectInFront;
    }

    private bool CanMove(int direction)
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1, 1 << 16 | 1 << 20);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1, 1 << 16 | 1 << 20);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1, 1 << 16 | 1 << 20);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1, 1 << 16 | 1 << 20);
        bool moveCheck = true;

        switch (direction)
        {
            case 1:
                if (hitLeft.collider == null)
                    moveCheck = true;
                else
                    moveCheck = false;
                break;
            case 2:
                if (hitRight.collider == null)
                    moveCheck = true;
                else
                    moveCheck = false;
                break;
            case 3:
                if (hitUp.collider == null)
                    moveCheck = true;
                else
                    moveCheck = false;
                break;
            case 4:
                if (hitDown.collider == null)
                    moveCheck = true;
                else
                    moveCheck = false;
                break;
            default:
                break;
        }
        return moveCheck;
    }

    private int GetKeyDirection()
    {
        int dir = 0;

        if (Input.GetKey(KeyCode.A))
        {
            dir = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = 2;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            dir = 3;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir = 4;
        }

        return dir;
    }

    public void MovePlayer()
    {
        int direction = GetKeyDirection();
        if (direction != 0)
            lastDirection = direction;
        bool canMove = CanMove(direction);

        if (direction == 1 && transform.position == targetPosition && canMove)
        {
            targetPosition += Vector3.left;
        }
        if (direction == 2 && transform.position == targetPosition && canMove)
        {
            targetPosition += Vector3.right;
        }
        if (direction == 3 && transform.position == targetPosition && canMove)
        {
            targetPosition += Vector3.up;
        }
        if (direction == 4 && transform.position == targetPosition && canMove)
        {
            targetPosition += Vector3.down;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

}
