using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouseFollow3D : MonoBehaviour
{
    public Transform _movingObj;
    public float _movementSpeed, _xlr8, _topSpeed, _baseSpeed;
    public bool _objCanMove;
    public MenuShift _doPanelSwap;
    [SerializeField] private GameObject _bSplasheffects;
    [SerializeField] private LookAt _objLook;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_objCanMove == true)
        {
            _movingObj.position += _movingObj.forward *  _movementSpeed * Time.fixedUnscaledDeltaTime;
            if (_movementSpeed >= _topSpeed)
            {
                _movementSpeed = _topSpeed;
            }
            else
            {
                _movementSpeed += _xlr8;
            }

        }
        else if (_objCanMove == false)
            _movingObj.position = _movingObj.position;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("menuObj"))
        {
            _doPanelSwap.BeginMenuMove();
        }
        else if (_other.CompareTag("Tracker"))
        {
            _objCanMove = false;
            _movementSpeed = _baseSpeed;
            //_bSplasheffects.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("menuObj"))
        {
            _doPanelSwap.StopMenuMove();
            
        }
        _objCanMove = true;
        //_bSplasheffects.SetActive(true);
    }
}
/*
public struct AnimationContainer
{
    public float AnimTime;
    public string AnimType;
};*/