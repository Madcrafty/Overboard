using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerJoinCustom : MonoBehaviour
{
    int count;
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p3;
    [SerializeField] private GameObject p4;
    [SerializeField] private SO_PlayerIDArray PID;
    [SerializeField] private GameObject EmptyPlayer;
    [SerializeField] private CharScriptableObject _charcterData;
    PlayerInputManager PIM;
    bool playerjoined = false;
    // Start is called before the first frame update
    void Start()
    {
        PIM = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    public void PlayerJoined(PlayerInput input)
    {
        if (count == 0)
        {
            PIM.playerPrefab = p2;
        }
        else if (count == 1)
        {
            PIM.playerPrefab = p3;
        }
        else if (count == 2)
        {
            PIM.playerPrefab = p4;
        }
        else
        {
            PIM.playerPrefab = p1;
        }
        count++;
    }
    public void NewPlayerJoined(PlayerInput input)
    {
        playerjoined = !playerjoined;
        PIM.playerPrefab = EmptyPlayer;
        if (playerjoined)
        {
            //PID.AddPlayer(input.GetInstanceID(), Random.Range(1,5));
            int player = PID.GetPlayer(input.GetInstanceID());
            switch (player)
            {
                case 1:
                    PIM.playerPrefab = p1;
                    break;
                case 2:
                    PIM.playerPrefab = p2;
                    break;
                case 3:
                    PIM.playerPrefab = p3;
                    break;
                case 4:
                    PIM.playerPrefab = p4;
                    break;
                default:
                    PIM.playerPrefab = p1;
                    break;
            }
            PIM.JoinPlayer();
        }
    }
}
