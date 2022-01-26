using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private InputAction[] _controlerInputs = new InputAction[0];
    [SerializeField] private GameObject[] _avaialableCharacterObjects = new GameObject[4];

    private  GameObject[] _selectedCharacters;
    private int[] _characterSelections = new int[4];

    void Start()
    { 
        _selectedCharacters = new GameObject[4];
    }

    void Update()
    {
        
    }

    void SaveCharacterSeleciton()
    {

    }
}
