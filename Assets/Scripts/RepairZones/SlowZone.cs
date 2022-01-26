using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowspeed = 1f;
    private List<PlayerController> Players = new List<PlayerController>();
    private void OnTriggerEnter(Collider other)
    {
        // This shit actiates after disable
        if (other.CompareTag("Player") && gameObject.activeSelf == true)
        {
            other.GetComponent<PlayerController>().SetEffectiveSpeed(slowspeed);
            Players.Add(other.GetComponent<PlayerController>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ResetEffectiveSpeed();
            Players.Remove(other.GetComponent<PlayerController>());
        }
    }
    public void Disable()
    {
        foreach (PlayerController item in Players)
        {
            item.ResetEffectiveSpeed();
        }
    }
    //private void OnDestroy()
    //{
    //    foreach (CharacterController item in Players)
    //    {
    //        item.ResetEffectiveSpeed();
    //    }
    //}
}
