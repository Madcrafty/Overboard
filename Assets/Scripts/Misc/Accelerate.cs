using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : MonoBehaviour
{
    public float _slipSpeedMulti = 200;
    public Rigidbody _rb;
    void Update()
    {
        _rb.AddForce(Vector3.right *  _slipSpeedMulti * Time.deltaTime, ForceMode.Force);
    }
}
