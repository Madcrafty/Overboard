using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToMouse : MonoBehaviour
{ 
    [SerializeField] private float _maxSpeed = .2f;
    [SerializeField] private float _speed = .2f;
    [SerializeField] private float _zDistance = 5.0f;
    [SerializeField] private Camera _screenCamera;

    void FixedUpdate()
    {
        var mousePos = Input.mousePosition;

        Vector3 target = _screenCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _zDistance));
        Vector3 direction = Vector3.Normalize(target - transform.position);
        if (Vector3.Distance(transform.position, target) > _maxSpeed)
            target = transform.position + (_maxSpeed * direction);
        transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
    }
}
