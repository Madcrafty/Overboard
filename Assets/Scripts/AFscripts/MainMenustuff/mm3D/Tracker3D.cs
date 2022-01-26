using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker3D : MonoBehaviour
{
    public Transform _trackerObj, _gamepadTransform;
    public bool _unclicked = true;
    public MenuShift _sendTo;
    public bool _controllerUsed = false;
    [SerializeField]
    private Camera _cam;
    private int _layerTarget = 1 << 11;


    void Update()
    {
        Ray _trvlRay = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_trvlRay, out RaycastHit _hit, 10000f, _layerTarget))
        {
            if (_hit.collider.gameObject != null)
            {
                _trackerObj.position = _hit.point;
            }
        }

        //if (_controllerUsed == false)
        //{
        //    if (_unclicked == true)
        //        _trackerObj.position = Input.mousePosition;
        //    else
        //        _trackerObj.position = _trackerObj.position;
        //}
        //else if (_controllerUsed == true)
        //{
        //    if (_unclicked == true)
        //        _trackerObj.position = _gamepadTransform.position;
        //    else
        //        _trackerObj.position = _trackerObj.position;
        //}
    }

    public void ClickedDestination(Transform y)
    {
        _unclicked = false;
        _trackerObj.position = y.position;
    }
    public void NextPanelTo(int _screenTo)
    {
        _sendTo._pId = _screenTo;
    }
    public void FollowMouseAgain()
    {
        _unclicked = true;
        _sendTo._pId = 0;
    }
}
