using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Pickup : MonoBehaviour
{
    public PlayerInput _PI;
    public float _ThrowPower = 5;
    private RaycastHit _info;
    private Ray _ray;
    private bool _hold = false;
    private GameObject _objectHeld;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPickup(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Physics.Raycast(transform.position + Vector3.up * 0.1f, transform.forward, out _info, 1f, LayerMask.GetMask("Default"));
            if (_hold)
            {
                if (value.interaction is TapInteraction)
                {
                    Drop();
                }
                else if (value.interaction is HoldInteraction)
                {
                    Throw();
                }
            }
            else
            {
                if (_info.collider != null && _info.collider.name != name)
                {
                    Grab(_info.transform.gameObject);
                }
            }
        }
    }
    void Grab(GameObject Suplies)
    {
        _objectHeld = Suplies;
        if (_objectHeld.GetComponent<SupplyCrate>() != null && _objectHeld.GetComponent<SupplyCrate>().Contents != null)
        {
            //Grab(_objectHeld.GetComponent<SupplyCrate>().Contents);
            _objectHeld = Instantiate(_objectHeld.GetComponent<SupplyCrate>().Contents);
        }
        if (_objectHeld.GetComponent<Rigidbody>() != null && _objectHeld.GetComponent<Rigidbody>().isKinematic == false)
        {
            _objectHeld.GetComponent<Rigidbody>().isKinematic = true;
        }
        _objectHeld.transform.SetParent(null);
        _objectHeld.transform.SetParent(transform);
        _objectHeld.transform.localPosition = Vector3.forward * 1.1f + Vector3.up * 0.75f;
        _hold = !_hold;
    }
    void Drop()
    {
        _objectHeld.transform.SetParent(null);
        if (_objectHeld.GetComponent<Rigidbody>() == null)
        {
            _objectHeld.gameObject.AddComponent<Rigidbody>();
        }
        else if (_objectHeld.GetComponent<Rigidbody>().isKinematic == true)
        {
            _objectHeld.GetComponent<Rigidbody>().isKinematic = false;
        }
        _objectHeld = null;
        _hold = !_hold;
    }
    void Throw()
    {
        _objectHeld.transform.SetParent(null);
        if (_objectHeld.GetComponent<Rigidbody>() == null)
        {
            _objectHeld.gameObject.AddComponent<Rigidbody>();
        }
        else if (_objectHeld.GetComponent<Rigidbody>().isKinematic == true)
        {
            _objectHeld.GetComponent<Rigidbody>().isKinematic = false;
        }
        _objectHeld.GetComponent<Rigidbody>().AddForce(transform.forward * _ThrowPower, ForceMode.VelocityChange);
        _objectHeld = null;
        _hold = !_hold;
    }
    public GameObject GetHeldObject()
    {
        return _objectHeld;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + transform.forward);
    }
}
