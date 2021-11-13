using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private bool _isPlayerCharacter;
    public bool IsPlayerCharacter { get => _isPlayerCharacter; }
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private CharacterStats stats;
    [SerializeField]
    private EquipmentManager equipment;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
