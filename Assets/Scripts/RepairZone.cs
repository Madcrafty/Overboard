using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairZone : MonoBehaviour
{
    public GameObject SuppliesToFix;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Pickup>().GetHeldObject().name == SuppliesToFix.name)
            {
                Debug.Log("repaired");
                Destroy(gameObject);
            }         
        }
    }
}
