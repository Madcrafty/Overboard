using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Pickup : MonoBehaviour
{
    public Mesh mesh;
    public Animator anim;
    public PlayerInput _PI;
    public Transform HoldTrans;
    public float _grabBaseRange = 1f;
    public float _grabRangeScale = 1f;
    public Vector3 _grabBoxDimensions;
    public float _ThrowPower = 5;
    public LayerMask _pickupLayerMask;
    public LayerMask _interactLayerMask;
    public bool GrabDisabled;
    public GameObject highlightPrefab;
    private RaycastHit _info;
    private bool _hold = false;
    private bool _pickingUp = false;
    private GameObject _objectHeld;
    private Rigidbody _objectRB;
    private GameObject _highlightLight;
    private SupplyCrate _supplyBox;
    private Vector3 _trueBoxDimensions;
    private float timer;
    private Vector3 StartPoint;
    private float _trueGrabRange;
    private Vector3 Endpoint;
    private void OnValidate()
    {
        SetUpGrab();
    }
    public void Start()
    {
        SetUpGrab();
        _highlightLight = Instantiate(highlightPrefab);
        _highlightLight.SetActive(false);
        //_highlightLight.transform.parent = gameObject.transform;
    }
    void SetUpGrab()
    {
        StartPoint = transform.position + transform.up * 0.1f;
        _trueGrabRange = _grabBaseRange * transform.localScale.magnitude * _grabRangeScale;
        Endpoint = StartPoint + transform.forward * _trueGrabRange;
        _trueBoxDimensions = new Vector3(_grabBoxDimensions.x * transform.localScale.x, _grabBoxDimensions.y * transform.localScale.y, _grabBoxDimensions.z * transform.localScale.z);
    }
    void Detection(LayerMask layerMask)
    {
        _trueGrabRange = _grabBaseRange * transform.localScale.magnitude * _grabRangeScale;
        Physics.BoxCast(StartPoint, _trueBoxDimensions, transform.forward, out _info, transform.rotation, _trueGrabRange, layerMask);
        if (_info.collider != null && _info.collider.name != name)
        {
            _supplyBox = _info.transform.GetComponent<SupplyCrate>();
        }
    }
    public void Update()
    {
        if (GetHeldObject() == null)
        {
            SetUpGrab();
            Detection(_pickupLayerMask);
            if (_info.collider != null && _info.collider.name != name)
            {
                if (_supplyBox != null)
                {
                    _supplyBox.UI.SetActive(true);
                }
                _highlightLight.SetActive(true);
                _highlightLight.transform.position = _info.transform.position;
            }
            else 
            {
                if (_supplyBox != null)
                {
                    _supplyBox.UI.SetActive(false);
                    _supplyBox = null;
                }
                _highlightLight.SetActive(false);
            }
        }
        else
        {
            if (_supplyBox != null)
            {
                _supplyBox.UI.SetActive(false);
                _supplyBox = null;
            }
            Detection(_interactLayerMask);
            if (_info.collider != null && _info.collider.name != name)
            {
                if (_info.transform.GetComponent<RepairZone>() != null && _info.transform.GetComponent<RepairZone>().CheckObject(GetHeldObject().name))
                {
                    //_supplyBox.UI.SetActive(true);
                    _highlightLight.SetActive(true);
                    _highlightLight.transform.position = _info.transform.position;
                }
                else
                {
                    //_supplyBox.UI.SetActive(false);
                    _highlightLight.SetActive(false);
                }
            }
            else
            {
                //_supplyBox.UI.SetActive(false);
                _highlightLight.SetActive(false);
            }
        }
        if (_pickingUp)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 1;
            }
            //Vector3 = _objectHeld
            _objectHeld.transform.localPosition = Vector3.Lerp(_objectHeld.transform.localPosition, Vector3.zero, timer);
            _objectHeld.transform.localRotation = Quaternion.Lerp(_objectHeld.transform.localRotation, Quaternion.Euler(Vector3.zero), timer);
            if (timer == 1)
            {
                timer = 0;
                _pickingUp = false;
            }
        }

    }
    public void OnPickup(InputAction.CallbackContext value)
    {
        if (value.performed && !GrabDisabled)
        {          
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
                Detection(_pickupLayerMask);
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
        // Supply Crate checks
        if (_objectHeld.GetComponent<SupplyCrate>() != null && _objectHeld.GetComponent<SupplyCrate>().Contents != null)
        {
            //Grab(_objectHeld.GetComponent<SupplyCrate>().Contents);
            GameObject tmp = Instantiate(_objectHeld.GetComponent<SupplyCrate>().Contents);
            tmp.transform.position = _objectHeld.transform.position;
            _objectHeld = tmp;
        }
        // Rigidbody Checks
        if (_objectHeld.GetComponent<Rigidbody>() != null && _objectHeld.GetComponent<Rigidbody>().isKinematic == false)
        {
            _objectHeld.GetComponent<Rigidbody>().isKinematic = true;
        }
        // Collider Checks
        if (_objectHeld.GetComponent<Collider>() != null && _objectHeld.GetComponent<Collider>().enabled == true)
        {
            _objectHeld.GetComponent<Collider>().enabled = false;
        }
        // OnPickup
        if (_objectHeld.GetComponent<OnPickup>() != null)
        {
            _objectHeld.GetComponent<OnPickup>().OnPickupEvent.Invoke(gameObject);
        }
        // transform stuff
        _objectHeld.transform.SetParent(null);
        _objectHeld.transform.SetParent(HoldTrans);
        // Set Variables
        _hold = true;
        _pickingUp = true;
        // Reset Triggers
        anim.ResetTrigger("Drop");
        anim.ResetTrigger("Throw");
        anim.ResetTrigger("Hammer");
        anim.ResetTrigger("Explode");
        // Set bool
        anim.SetBool("PickUp", _hold);
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
        // Collider Checks
        if (_objectHeld.GetComponent<Collider>().enabled == false)
        {
            _objectHeld.GetComponent<Collider>().enabled = true;
        }
        if (_objectHeld.GetComponent<OnPickup>() != null)
        {
            _objectHeld.GetComponent<OnPickup>().OnDropEvent.Invoke(gameObject);
        }
        _objectHeld = null;
        _hold = false;
        _pickingUp = false;
        anim.SetTrigger("Drop");
        anim.SetBool("PickUp", _hold);
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
        // Collider Checks
        if (_objectHeld.GetComponent<Collider>().enabled == false)
        {
            _objectHeld.GetComponent<Collider>().enabled = true;
        }
        if (_objectHeld.GetComponent<OnPickup>() != null)
        {
            _objectHeld.GetComponent<OnPickup>().OnThrowEvent.Invoke(gameObject);
        }
        _objectHeld.GetComponent<Rigidbody>().AddForce(transform.forward * _ThrowPower, ForceMode.VelocityChange);
        _objectHeld = null;
        _hold = false;
        _pickingUp = false;
        anim.SetTrigger("Throw");
        anim.SetBool("PickUp", _hold);
    }
    public void RemoveObject()
    {
        Destroy(_objectHeld);
        if (_objectHeld.GetComponent<OnPickup>() != null)
        {
            _objectHeld.GetComponent<OnPickup>().OnDropEvent.Invoke(gameObject);
        }
        _objectHeld = null;
        _hold = false;
        _pickingUp = false;
        anim.SetBool("PickUp", _hold);
    }
    public GameObject GetHeldObject()
    {
        return _objectHeld;
    }
    public void OnDrawGizmos()
    {        
        //Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + transform.forward * _grabRangeScale * transform.localScale.z);
        Gizmos.DrawLine(StartPoint, Endpoint);
        //Gizmos.DrawCube(Endpoint, _grabBoxDimensions);
        Gizmos.DrawWireMesh(mesh, Endpoint, transform.rotation, _trueBoxDimensions * 2f);
    }
}