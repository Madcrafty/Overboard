using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTriggerReset : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ResetPosition();
        }
        if (other.CompareTag("Tool"))
        {
            other.GetComponent<Tool>().Respawn();
        }
    }
}
