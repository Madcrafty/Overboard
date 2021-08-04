using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShift : MonoBehaviour
{
    public int _pId = 0;
    public Transform _panelHolder;
    public int _shiftSpeed;
    private bool _canMove = false;

    private void Update()
    {
        if (_canMove ==  true)
        {
            if (_pId == 1)
            {
                _panelHolder.transform.position += _panelHolder.transform.up * Time.deltaTime * _shiftSpeed * -1;
                //if ()
                //{
                //    _canMove = false;
                //}
            }
            else if (_pId == 2)
            {
                _panelHolder.transform.position += _panelHolder.transform.right * Time.deltaTime * _shiftSpeed;
            }
            else if (_pId == 3)
            {
                _panelHolder.transform.position += _panelHolder.transform.right * Time.deltaTime * _shiftSpeed * -1;
            }
            else if (_pId == 4)
            {
                //open a "are you sure" panel
            }
        }
        
    }
    public void BeginMenuMove()
    {
        _canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StopMenuMove()
    {
        _canMove = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
