using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerInputData", menuName = "ScriptableObjects/PlayerInputArray")]
public class SO_PlayerIDArray : ScriptableObject
{
    public class IDbyPlayerNum
    {
        public int inputID, player;
        public bool charAvailable = true;
        public IDbyPlayerNum(int a_InputID, int a_Player)
        {
            inputID = a_InputID;
            player = a_Player;
            charAvailable = true;
        }
    }

    public List<IDbyPlayerNum> _inputIDbyPlayers;
    public List<GameObject> _characterObjects = new List<GameObject>();

    private void OnEnable()
    {
        _inputIDbyPlayers.Add(new IDbyPlayerNum(0, -1));
        _inputIDbyPlayers.Add(new IDbyPlayerNum(0, -1));
        _inputIDbyPlayers.Add(new IDbyPlayerNum(0, -1));
        _inputIDbyPlayers.Add(new IDbyPlayerNum(0, -1));
    }

    public void AddPlayer(int inputID, int player)
    {
        _inputIDbyPlayers.Add(new IDbyPlayerNum(inputID, player));
    }
    public int GetPlayer(int inputID)
    {
        int tmp = 0;
        for (int i = 0; i < _inputIDbyPlayers.Count; i++)
        {
            if (_inputIDbyPlayers[i].inputID == inputID)
            {
                tmp = _inputIDbyPlayers[i].player;
                break;
            }
        }
        return tmp;
    }

    public int AvailableCount()
    {
        int counter = 0;
        for (int i = 0; i < _inputIDbyPlayers.Count; i++)
        {
            if (_inputIDbyPlayers[i].charAvailable == true)
                counter++;
        }
        return counter;
    }

    public void Clear()
    {
        _inputIDbyPlayers.Clear();
    }
}
