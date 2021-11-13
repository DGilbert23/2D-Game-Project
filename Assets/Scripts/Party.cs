using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField]
    private Character[] partyMembers;
    private static Party partyInstance;

    //Movement variables
    private int speed = 8;
    [SerializeField]
    private LayerMask stopMovementLayers;
    private Vector3 targetPosition;
    public int lastDirection;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (partyInstance == null)
            partyInstance = this;
        else
            Destroy(gameObject);
    }

    public void MovePartyMembers()
    {
        foreach(Character person in partyMembers)
        {

        }
    }
}
