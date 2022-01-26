using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnableAndDisable : MonoBehaviour
{   
    public void EnableObject()
    {
        GetComponent<TextMeshPro>().enabled = true;
    }

    public void DisableObject()
    {
        GetComponent<TextMeshPro>().enabled = false;

    }
}
