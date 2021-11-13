using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int speed = 8;
    private Vector3 targetPosition;
    private bool movedSinceLastTrigger = true;
    public LayerMask stopMovementLayer;
    public int lastDirection;
    [SerializeField]
    private GameObject inBuildingFog;
    [SerializeField]
    private GameObject outOfBuildingFog;

    void Start()
    {
        targetPosition = transform.position;
        if(inBuildingFog == null)        
            inBuildingFog = GameObject.Find("InBuildingFog");        
        if (outOfBuildingFog == null)
            outOfBuildingFog = GameObject.Find("OutOfBuildingFog");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

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
                    hitObject = Physics2D.Raycast(transform.position + new Vector3(.2f, .2f, .2f), Vector2.left, .8f);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                case 2:
                    hitObject = Physics2D.Raycast(transform.position + new Vector3(.2f, .2f, .2f), Vector2.right, .8f);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                case 3:
                    hitObject = Physics2D.Raycast(transform.position + new Vector3(.2f, .2f, .2f), Vector2.up, .8f);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                case 4:
                    hitObject = Physics2D.Raycast(transform.position + new Vector3(.2f, .2f, .2f), Vector2.down, .8f);
                    if (hitObject.transform.gameObject != null)
                        objectInFront = hitObject.transform.gameObject;
                    break;
                default:
                    objectInFront = null;
                    break;
            }

            Debug.Log(objectInFront.name);
        }
        catch (System.NullReferenceException e)
        {
            objectInFront = null;
        }

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

        if (Input.GetAxisRaw("Horizontal") == -1)
            dir = 1;
        else if (Input.GetAxisRaw("Horizontal") == 1)
            dir = 2;
        else if (Input.GetAxisRaw("Vertical") == 1)
            dir = 3;
        else if (Input.GetAxisRaw("Vertical") == -1)
            dir = 4;        

        return dir;
    }

    public void ResetMoveTarget()
    {
        targetPosition = this.transform.position;
    }

    public void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) == 0f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(targetPosition + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopMovementLayer))
                {
                    targetPosition += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                lastDirection = GetKeyDirection();
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(targetPosition + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopMovementLayer))
                {
                    targetPosition += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                lastDirection = GetKeyDirection();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.tag)
        {
            case "TransitionObject":
                GameObject.Find("GameManager").GetComponent<GameManager>().HandleTransitionObject(collision.name);
                break;
            case "FogTransition":
                if(inBuildingFog.activeInHierarchy)
                {
                    inBuildingFog.SetActive(false);
                    outOfBuildingFog.SetActive(true);
                }
                else
                {
                    inBuildingFog.SetActive(true);
                    outOfBuildingFog.SetActive(false);
                }
                break;

            default:
                Debug.Log("OnTriggerEnter2D with object with no tag. Mistake?");
                break;
        }
    }    

}
