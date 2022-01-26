using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipZone : MonoBehaviour
{
    public Movement movementType;
    private List<PlayerController> Players = new List<PlayerController>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.activeSelf == true)
        {
            other.GetComponent<PlayerController>().grdMovement = movementType;
            Players.Add(other.GetComponent<PlayerController>());
            if (other.GetComponent<FireEffect>() != null)
            {
                other.gameObject.GetComponent<FireEffect>().Extinguish();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ResetGroundMovement();
            Players.Remove(other.GetComponent<PlayerController>());
        }
    }
    public void Disable()
    {
        foreach (PlayerController item in Players)
        {
            item.ResetGroundMovement();
        }
    }
    //private void OnDestroy()
    //{
    //    foreach (PlayerController item in Players)
    //    {
    //        item.ResetGroundMovement();
    //    }
    //}
}
