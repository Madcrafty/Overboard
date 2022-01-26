using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoatFollow : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private float _zDistance = 1;
    [SerializeField] private Camera _mainCam;

    private void Start()
    {
        transform.rotation.SetLookRotation(transform.forward, transform.right);
    }

    void FixedUpdate()
    {
        var mousePos = Input.mousePosition;

        Vector3 mouseWorldPos = _mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - _mainCam.transform.position.z));

        Vector3 vectorToTarget = mouseWorldPos - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        Quaternion lookRot = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);//Quaternion.LookRotation(mouseWorldPos - transform.position);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, _rotationSpeed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, mouseWorldPos);
        Vector3 target = transform.position + transform.forward * dist;
        if (dist > _speed)
            target = transform.position + transform.forward * _speed;
        transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
    }
}
