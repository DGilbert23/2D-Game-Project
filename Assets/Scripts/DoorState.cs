using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState 
{
    private string _name;
    public string Name
    {
        get => _name;
    }
    public bool isOpen;
    public bool isLocked;

    public DoorState(string inName, bool inIsOpen, bool inIsLocked)
    {
        _name = inName;
        isOpen = inIsOpen;
        isLocked = inIsLocked;
    }

    public override string ToString()
    {
        return Name;
    }

}
