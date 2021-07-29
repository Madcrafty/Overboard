using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public PlayerInput _PI;
    public float speed = 3f;
    private Vector3 _lastGroundedPosition;
    private Transform _spawnPoint;
    private Collider _collider;
    private RaycastHit _info;
    private Ray _ray;
    private Vector3 rawInputMovement;
    private Rigidbody _rigidbody;
    private float airtime;
    // Start is called before the first frame update
    private void Awake()
    {
        //_ray = new Ray(transform.position,)
        _collider = GetComponent<Collider>();
        _lastGroundedPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        transform.LookAt(transform.position + rawInputMovement);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isGrounded())
        {
            //if (airtime > 0)
            //{
            //    airtime = 0;
            //}
            _lastGroundedPosition = transform.position;
            _rigidbody.MovePosition(transform.position + (rawInputMovement * speed * Time.deltaTime));
        }
        else
        {
            //airtime += Time.deltaTime;
            _rigidbody.MovePosition(transform.position + (rawInputMovement * speed * Time.deltaTime));
            //_rigidbody.MovePosition(Vector3.down * 9.8f * airtime * Time.deltaTime);
        }
    }
    bool isGrounded()
    {
        Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out _info, 0.2f, LayerMask.GetMask("Default"));
        if (_info.collider != null && _info.collider.name != name)
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
        //Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * 0.2f);
        //Gizmos.DrawLine(transform.position, new Vector3()
    }
}
