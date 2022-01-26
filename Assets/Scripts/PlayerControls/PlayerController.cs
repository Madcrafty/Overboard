using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    public bool findSpawnPoint;
    public bool findPauseMenu;
    public PlayerInput _PI;
    public InGamePanel _PM;
    public Animator anim;
    public LayerMask GroundLayer;
    public Movement grdMovement;
    public Movement airMovement;
    public float speed = 3f;
    public float repairPower = 0.2f;
    public Transform InitSpawn;
    public AudioSource _as;
    private List<Action<GameObject>> ContinuousHoldUpdate = new List<Action<GameObject>>();
    //public UnityEvent Interaction;
    //public delegate void InteractDelegate(GameObject obj);
    //private InteractDelegate Interact;
    public Action<GameObject> InteractAction;
    private Vector3 _lastGroundedPosition;
    private Transform _spawnPoint;
    private Collider _collider;
    private RaycastHit _info;
    //private Ray _ray;
    private Vector3 rawInputMovement;
    private Rigidbody _rigidbody;
    private float airtime;
    private float _effectiveSpeed;
    private Movement _originalGrdMovement;
    private Movement _originalAirMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        //_ray = new Ray(transform.position,)
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _originalGrdMovement = grdMovement;
        _originalAirMovement = airMovement;
        if (findSpawnPoint && GameObject.Find("CharacterSpawn"))
        {
            InitSpawn = GameObject.Find("CharacterSpawn").transform;
            transform.position = InitSpawn.position;
            transform.localScale = InitSpawn.localScale;
            _lastGroundedPosition = InitSpawn.position;
            _spawnPoint = InitSpawn;
        }
        else if (findSpawnPoint)
        {
            Debug.LogWarning("Can't find CharacterSpawn");
        }
        _effectiveSpeed = speed;
        if (findPauseMenu && GameObject.Find("INGAMEUI"))
        {
            _PM = GameObject.Find("INGAMEUI").GetComponentInChildren<InGamePanel>();
        }
        else if (findPauseMenu)
        {
            Debug.LogWarning("Can't find pause Menu");
        }
        
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        transform.LookAt(transform.position + rawInputMovement);
        float speedmod = Mathf.Sqrt(Mathf.Pow(inputMovement.x, 2) + Mathf.Pow(inputMovement.y, 2));
        anim.SetFloat("Speed", _effectiveSpeed * speedmod);
    }
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.performed == true && _PM != null)
        {
            _PM.MenuToggle();
            Debug.Log("working pause" + name);
        }
    }
    public void OnInteraction(InputAction.CallbackContext value)
    {
        if (value.performed == true && value.interaction is PressInteraction)
        {
            if (InteractAction != null)
            {
                //InteractAction.Invoke(gameObject);
                InteractAction.Invoke(gameObject);
                Debug.Log("interaction call");
            }
        }
        //if (value.canceled == true)
        //    if (InteractAction != null)
        //        ContinuousHoldUpdate.Clear(); Debug.Log("interaction let go");

    }
    public void AssignInteraction(Action<GameObject> func)
    {
        InteractAction += func;
    }
    public void RemoveInteraction(Action<GameObject> func)
    {
        InteractAction -= func;
        // Calling this function here still allows things to work but may bring up an error
        ContinuousHoldUpdate.Clear();
    }
    public void Test(GameObject tmp)
    {

    }
    // Update is called once per frame
    void Update()
    {
        foreach (Action<GameObject> item in ContinuousHoldUpdate)
        {
            item.Invoke(gameObject);
        }
    }
    public void PlayRandomHitSound(AudioClip[] Clips)
    {
        //using UnityEngine.Random.range
        _as.clip = Clips[UnityEngine.Random.Range(0, Clips.Length)];
        _as.Play();
    }
    public void SetEffectiveSpeed(float a_Speed)
    {
        _effectiveSpeed = a_Speed;
    }
    public void ResetEffectiveSpeed()
    {
        _effectiveSpeed = speed;
    }
    public void ResetGroundMovement()
    {
        grdMovement = _originalGrdMovement;
    }
    public void ResetAirMovement()
    {
        airMovement = _originalAirMovement;
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
            grdMovement.Move(_rigidbody, rawInputMovement, _effectiveSpeed * transform.localScale.y);
            //_rigidbody.MovePosition(transform.position + (rawInputMovement * _effectiveSpeed * Time.deltaTime));
            //_rigidbody.AddForce(rawInputMovement * _effectiveSpeed * _slipSpeedMulti * Time.deltaTime, ForceMode.Force);
        }
        else
        {
            //airtime += Time.deltaTime;
            airMovement.Move(_rigidbody, rawInputMovement, _effectiveSpeed * transform.localScale.y);
        }
    }
    bool isGrounded()
    {
        if (_collider.isTrigger == false)
        {
            Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out _info, 0.2f, GroundLayer);
            if (_info.collider != null && _info.collider.name != name)
            {
                return true;
            }
        }
        return false;
    }
    public void ResetPosition()
    {
        if (_lastGroundedPosition == null)
        {
            Debug.LogWarning("No grounded position set");
            Debug.Log("Attempting Respawn()");
            Respawn();
            return;
        }
        transform.position = _lastGroundedPosition;
        _rigidbody.velocity = Vector3.zero;
    }
    public void Respawn()
    {
        if (_spawnPoint == null)
        {
            Debug.LogWarning("No spawn position set");
            transform.position = Vector3.zero;
            return;
        }
        transform.position = _spawnPoint.position;
        _rigidbody.velocity = Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * 0.2f);
        //Gizmos.DrawLine(transform.position, new Vector3()
    }
}
