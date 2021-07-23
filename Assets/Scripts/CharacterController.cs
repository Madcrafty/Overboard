using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 _lastGroundedPosition;
    private Transform _spawnPoint;
    private Collider _collider;
    private RaycastHit _info;
    // Start is called before the first frame update
    
    void Start()
    {
        _collider = GetComponent<Collider>();
        _lastGroundedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isGrounded())
        {
            _lastGroundedPosition = transform.position;
        }
    }
    bool isGrounded()
    {
        Physics.Raycast(transform.position, Vector3.down, out _info, 0.1f, LayerMask.GetMask("Default"));
        if (_info.collider != null && _info.collider.name != name && _info.distance <= 0.1f)
        {
            return true;
        }
        return false;
    }
    public void ResetPosition()
    {
        transform.position = _lastGroundedPosition;
    }
    public void Respawn()
    {
        transform.position = _spawnPoint.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.1f);
        //Gizmos.DrawLine(transform.position, new Vector3()
    }
}
