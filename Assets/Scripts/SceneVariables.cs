using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVariables
{
    private string _sceneName;
    public string sceneName
    {
        get => _sceneName;
    }
    private List<DoorState> doorStates = new List<DoorState>();

    public SceneVariables(string inName)
    {
        _sceneName = inName;
    }

    public override string ToString()
    {
        return sceneName;
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
        DoorState currentState = GetDoorStateByName(inDoorState.name);
        if(currentState == null)
        {
            doorStates.Add(inDoorState);
            Debug.Log("Added DoorState with name" + inDoorState.name);
        }
        else
        {
            currentState.isLocked = inDoorState.isLocked;
            currentState.isOpen = inDoorState.isOpen;
            Debug.Log("Updated DoorState with name" + inDoorState.name);
        }

    }

    public void VerifyDoorStates()
    {
        Debug.Log(this.sceneName);
        Debug.Log(GameStateVariables.GetCurrentSceneName());
        if(GameStateVariables.GetCurrentSceneName() == this.sceneName)
        {
            foreach (DoorState i in doorStates)
            {
                Interactable currObject = GameObject.Find(i.name).GetComponent<Interactable>();
                if(currObject != null)
                {
                    currObject.open = i.isOpen;
                    currObject.locked = i.isLocked;
                }
            }
        }
        else
        {
            Debug.Log("Trying to verify Door States for variables in a non-current Scene. How did we get here?");
        }
    }
}
