using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour
{
    public Transform _toLookAt;
    public Transform _objLooking;
    
    // Update is called once per frame
    void Update()
    {
        _objLooking.transform.LookAt(_toLookAt, Vector3.up);
    }
}
