using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedChange : MonoBehaviour
{
    public float _newSpeed;
    public ObjectMouseFollow _targetSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        _targetSpeed._movementSpeed = _newSpeed;
    }
}
