using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MenuShift : MonoBehaviour
{
    public int _pId = 0;
    public Transform _movingObj, _pointMove1, _pointMove2, _pointMove3, _pointMove4, _cameraObj;
    private Vector3 _P1Origin, _P2Origin, _P3Origin, _P4Origin;
    public int _shiftSpeed;
    private bool _canMove = false, _backToMainMenu;
    public int _sceneTo;
    public ObjectMouseFollow _mouseFollowObj;
    public Tracker _trackerObj1;
    public GameObject _quitCheck, _boat;
    [SerializeField]
    private GameObject _virtualMouseObj;
    [SerializeField]
    private GameObject _virtMouseImage;
    [SerializeField]
    private GamepadCursor _vmouseScript;

    private void Start()
    {
        _P1Origin = _pointMove1.position;
        _P2Origin = _pointMove2.position;
        _P3Origin = _pointMove3.position;
        _P4Origin = _pointMove4.position;
    }

    private void Update()
    {
        if (_canMove ==  true)
        {
            if (_pId == 1)
            {
                //level select
                Cursor.lockState = CursorLockMode.Locked;
                UpdateVMouse();
                _movingObj.transform.position += _movingObj.transform.up * Time.deltaTime * _shiftSpeed * -1;
                if (_pointMove2.transform.position.y <= _cameraObj.position.y)
                {
                    StopPanelMovement(_pointMove2);
                }
            }
            else if (_pId == 2)
            {
                //Options
                Cursor.lockState = CursorLockMode.Locked;
                _movingObj.transform.position += _movingObj.transform.right * Time.deltaTime * _shiftSpeed;
                if (_pointMove3.transform.position.x >= _cameraObj.position.x)
                {
                    StopPanelMovement(_pointMove3);
                }
            }
            else if (_pId == 3)
            {
                //credits
                Cursor.lockState = CursorLockMode.Locked;
                _movingObj.transform.position += _movingObj.transform.right * Time.deltaTime * _shiftSpeed * -1;
                if (_pointMove4.transform.position.x <= _cameraObj.position.x)
                {
                    StopPanelMovement(_pointMove4);
                }
            }
            else if (_pId == 4)
            {
                //quit
                //open a "are you sure" panel
            }
            else if (_pId == 5)
            {
                //back from options
                _canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                _movingObj.transform.position += _movingObj.transform.right * Time.deltaTime * _shiftSpeed * -1;
                if (_pointMove1.transform.position.x <= _cameraObj.position.x)
                {
                    _backToMainMenu = true;
                    ResetTrans();
                    StopPanelMovement(_pointMove1);
                }
                
            }
            else if (_pId == 6)
            {
                Debug.Log("yee");
                //back from level select
                _canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                _movingObj.transform.position += _movingObj.transform.up * Time.deltaTime * _shiftSpeed;
                if (_pointMove1.transform.position.y >= _cameraObj.position.y)
                {
                    _backToMainMenu = true;
                    ResetTrans();
                    StopPanelMovement(_pointMove1);
                }
            }
            else if (_pId == 7)
            {
                //back from credits
                _canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                _movingObj.transform.position += _movingObj.transform.right * Time.deltaTime * _shiftSpeed;
                if (_pointMove1.transform.position.x >= _cameraObj.position.x)
                {
                    _backToMainMenu = true;
                    ResetTrans();
                    StopPanelMovement(_pointMove1);
                }
            }
        }
    }
    public void BeginMenuMove()
    {
        _canMove = true;
        if (_pId != 0 && _pId != 4)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (_pId == 4)
        {
            QuitButton(1);
        }
    }

    public void StopMenuMove()
    {
        _canMove = false;
        Cursor.lockState = CursorLockMode.None;
        _trackerObj1.FollowMouseAgain();
    }

    public void StopPanelMovement(Transform _movingTo)
    {
        _movingTo.transform.position = _cameraObj.position;
        _pId = 0;
        Cursor.lockState = CursorLockMode.None;
        _trackerObj1._unclicked = true;
        _trackerObj1.FollowMouseAgain();
        if (_backToMainMenu == true)
        {
            _backToMainMenu = false;
        }
    }
    private void ResetTrans()
    {
        _pointMove1.position = _P1Origin;
        _pointMove2.position = _P2Origin;
        _pointMove3.position = _P3Origin;
        _pointMove4.position = _P4Origin;
    }

    public void QuitButton(int _Id)
    {
        if (_Id == 1)
        {
            //open menu, stop boat movement
            _quitCheck.SetActive(true);
            _boat.SetActive(false);
            _trackerObj1._unclicked = true;
            _pId = 0;
        }
        if (_Id == 2)
        {
            //No option for check panel
            _quitCheck.SetActive(false);
            _boat.SetActive(true);
        }
        if (_Id == 3)
        {
            //yes option
            Application.Quit();
            Debug.Log("quitsgame");
        }
    }

    private void UpdateVMouse()
    {
        _virtualMouseObj.transform.position = Input.mousePosition;
        _virtMouseImage.transform.position = Input.mousePosition;
        //_vmouseScript._virtualMouse = Input.mousePosition
    }
}
