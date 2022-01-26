using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    private Vector3 spawnPos;
    private Quaternion spawnRotation;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        spawnRotation = transform.rotation;
    }
    public void Respawn()
    {
        transform.position = spawnPos;
        transform.rotation = spawnRotation;
    }
}
