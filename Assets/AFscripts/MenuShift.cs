using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShift : MonoBehaviour
{
    public int _pId = 0;
    public RectTransform _panelIdOne, _panelIdTwo, _panelIdThree, _panelHolder;
    public int _shiftSpeed;
    private bool _canMove = false;

    private void Update()
    {
        if (_canMove ==  true)
        {
            if (_pId == 1)
            {
                _panelHolder.transform.position += _panelHolder.transform.up * Time.deltaTime * _shiftSpeed * -1;
                if (_panelHolder.rect.yMax >= 0)
                {
                    _canMove = false;
                    _panelHolder. = 1080;
                }
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
    }

    public void StopMenuMove()
    {
        _canMove = false;
    }
}
