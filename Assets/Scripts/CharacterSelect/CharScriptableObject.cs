using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterSelectData", order = 1)]
public class CharScriptableObject : ScriptableObject
{
    public class charObject
    {
        public charObject(int a_playernum, bool a_availability)
        {
            playerNum = a_playernum;
            charAvailable = a_availability;
        }

        public int playerNum;
        public bool charAvailable;
    }

    public List<charObject> _characterAvailable = new List<charObject>();

    public List<GameObject> _characterObjects = new List<GameObject>();

    private void OnEnable()
    {
        _characterAvailable.Add(new charObject(-1, true));
        _characterAvailable.Add(new charObject(-1, true));
        _characterAvailable.Add(new charObject(-1, true));
        _characterAvailable.Add(new charObject(-1, true));
    }

    public int AvailableCount()
    {
        int counter = 0;
        for (int i = 0; i < _characterAvailable.Count; i++)
        {
            if (_characterAvailable[i].charAvailable == true)
                counter++;
        }
        return counter;
    }
}

