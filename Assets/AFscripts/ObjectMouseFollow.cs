using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouseFollow : MonoBehaviour
{
    public Transform _movingObj;
    public float _movementSpeed;
    public bool _canMove;
    public MenuShift _doPanelSwap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove == true)
            _movingObj.transform.position += _movingObj.transform.forward * Time.deltaTime * _movementSpeed;
        else if (_canMove == false)
            _movingObj.transform.position = _movingObj.transform.position;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (!_other.CompareTag("menuObj"))
        {
            _doPanelSwap.NewMenuPanel();
        }
        else
            _canMove = false;
    }
    private void OnTriggerExit(Collider _other)
    {
        _canMove = true;
    }
}
