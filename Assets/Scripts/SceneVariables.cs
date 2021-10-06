using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVariables
{
    private string _name;
    public string name
    {
        get => _name;
    }
    private List<DoorState> doorStates = new List<DoorState>();

    public SceneVariables(string inName)
    {
        _name = inName;
    }

    public override string ToString()
    {
        return name;
    }

    public DoorState GetDoorStateByName(string doorName)
    {
        DoorState doorStateToReturn = null;
        bool found = false;
        int i = 0;
        while (i < doorStates.Count && !found)
        {
            if (doorStates[i].ToString() == doorName)
            {
                doorStateToReturn = doorStates[i];
                found = true;
            }
            else
            {
                i++;
            }
        }

        return doorStateToReturn;
    }

    public void UpdateDoorState(string doorName, bool inOpenState, bool inLockedState)
    {
        DoorState doorStateToChange = GetDoorStateByName(doorName);
        if(doorStateToChange == null)
        {
            doorStates.Add(new DoorState(doorName, inOpenState, inLockedState));
        }
        else
        {
            doorStateToChange.isOpen = inOpenState;
            doorStateToChange.isLocked = inLockedState;
        }
    }

    public void UpdateDoorState(DoorState inDoorState)
    {
        Debug.Log("isOpen?: " + inDoorState.isOpen + " :isLocked?: " + inDoorState.isLocked);
        DoorState currentState = GetDoorStateByName(inDoorState.name);
        if(currentState == null)
        {
            doorStates.Add(inDoorState);
        }
        else
        {
            currentState.isLocked = inDoorState.isLocked;
            currentState.isOpen = inDoorState.isOpen;
        }

    }

    public void VerifyDoorStates()
    {
        if(GameStateVariables.GetCurrentSceneName() == this.name)
        {
            foreach (DoorState i in doorStates)
            {
                Interactable currObject = GameObject.Find(i.name).GetComponent<Interactable>();
                if(currObject != null)
                {
                    Debug.Log("Found an object by the name " + i.name + ". Setting open & locked flags");
                    if (i.isLocked == false && currObject.locked == true)
                        currObject.Unlock(false);
                    if (i.isOpen == true && currObject.open == false)
                        currObject.OpenDoor(false);

                    Debug.Log("open?: " + currObject.open + " :locked?: " + currObject.locked);
                }
            }
        }
        else
        {
            Debug.Log("Trying to verify Door States for variables in a non-current Scene. How did we get here?");
        }
    }
}
