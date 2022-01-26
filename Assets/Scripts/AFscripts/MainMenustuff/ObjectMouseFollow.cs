using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouseFollow : MonoBehaviour
{
    public Transform _movingObj;
    public float _movementSpeed, _xlr8, _topSpeed, _baseSpeed;
    public bool _objCanMove;
    public MenuShift _doPanelSwap;
    [SerializeField] private GameObject _bSplasheffects;

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
        //Debug.Log("hit");
        if (_other.CompareTag("menuObj"))
        {
            _doPanelSwap.BeginMenuMove();

            /*switch(_other.tag.ToString())
            {
                case "Breakable":
                     break;
                case "Sinkable":
                    AnimationContainer Container;
                    Container.AnimType = "Sinkable";
                    Logic.PlayAnimationCLip(Container);
                    break;
            }*/
            
        }
        else
        {
            _objCanMove = false;
            _movementSpeed = _baseSpeed;
            _bSplasheffects.SetActive(false);
        }

    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("menuObj"))
        {
            _doPanelSwap.StopMenuMove();
            
        }
        _objCanMove = true;
        _bSplasheffects.SetActive(true);
    }
}
/*
public struct AnimationContainer
{
    public float AnimTime;
    public string AnimType;
};*/