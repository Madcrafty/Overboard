using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private List<Collider> Players = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.isTrigger = true;
            Players.Add(other);
        }
        //if (other.CompareTag("Player") && gameObject.activeSelf == true)
        //{
        //    other.isTrigger = true;
        //    Players.Add(other);
        //    //other.attachedRigidbody.AddForce(new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z) * 100, ForceMode.Force);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.isTrigger = false;
            Players.Remove(other);
        }
    }
    public void Disable()
    {
        foreach (Collider item in Players)
        {
            item.isTrigger = false;
        }
    }
    //private void OnDestroy()
    //{
    //    foreach (Collider item in Players)
    //    {
    //        item.isTrigger = false;
    //    }
    //}
}
