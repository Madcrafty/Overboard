using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;
public class InGamePanel : MonoBehaviour
{
    public GameObject _menuHolder, _optionsPanel, _quitPanel, _mMPanel, _mainPause;
    public Transform _panelHolder, _centrePoint, _mainPoint, _secondaryPoint, _originPoint;
    public bool _isMenuOpen = false;
    public bool _moveToSpot = false;
    public int _moveSpeed, _mId = 0;
    public PlayerInput _playerInput;
    public bool _mainMenuOpen = false;
    public void MenuToggle()
    {
        if (_mainMenuOpen == false)
        {
            _isMenuOpen = !_isMenuOpen;

            if (_isMenuOpen == true)
            {
                Time.timeScale = 0;
                _menuHolder.SetActive(true);
                _moveToSpot = true;
                _mId = 0;
                if (_panelHolder.position != _originPoint.position)
                {
                    _panelHolder.position = _originPoint.position;
                }
            }
            else if (_isMenuOpen == false)
            {
                Time.timeScale = 1;
                _panelHolder.position = _originPoint.position;
                _menuHolder.SetActive(false);
            }
        }
        
    }
    private void Update()
    {
        if (_moveToSpot == true)
        {
            float timeAddition = Time.unscaledDeltaTime;
            if (_mId == 0) //moves from bottom of screen up when opening pause menu
            {
                _mainPause.SetActive(true);
                _panelHolder.transform.position += (_panelHolder.transform.up * _moveSpeed) * timeAddition;
                if (_mainPoint.position.y >= _centrePoint.position.y)
                {
                    _moveToSpot = false;
                    _mainPause.SetActive(true);
                }
            }
            else if (_mId == 1) //moves down to second panel
            {
                _panelHolder.transform.position += (_panelHolder.transform.up * _moveSpeed) * timeAddition;
                if (_secondaryPoint.position.y >= _centrePoint.position.y)
                {
                    _moveToSpot = false;
                    _mainPause.SetActive(false);
                }
            }
            else if (_mId == 2) // moves back to main screen
            {
                _mainPause.SetActive(true);
                _panelHolder.transform.position += (_panelHolder.transform.up * _moveSpeed) * timeAddition * -1;
                if (_mainPoint.position.y <= _centrePoint.position.y)
                {
                    _moveToSpot = false;
                }
            }
        }
    }

    public void SetNewPanel(int _id)
    {
        //resume
        if (_id == 1)
        {
            //_isMenuOpen = !_isMenuOpen;
            _panelHolder.position = _originPoint.position;
            _menuHolder.SetActive(false);
            MenuToggle();
        }
        //Options
        else if (_id == 2)
        {
            _optionsPanel.SetActive(true);
            _mMPanel.SetActive(false);
            _quitPanel.SetActive(false);
            _mId = 1;
            _moveToSpot = true;
        }
        //main menu
        else if (_id == 3)
        {
            _optionsPanel.SetActive(false);
            _mMPanel.SetActive(true);
            _quitPanel.SetActive(false);
            _mId = 1;
            _moveToSpot = true;
        }
        //Quit game
        else if (_id == 4)
        {
            _optionsPanel.SetActive(false);
            _mMPanel.SetActive(false);
            _quitPanel.SetActive(true);
            _mId = 1;
            _moveToSpot = true;

        }
        //back to main screen
        else if (_id == 5)
        {

            _mId = 2;
            _moveToSpot = true;
        }
        //main menu yes
        else if (_id == 6)
        {
            Time.timeScale = 1;
            //SceneManager.LoadScene(0);
        }
        //quit game yes
        else if (_id == 7)
        {
            Application.Quit();
            Debug.Log("quitsgame");
        }
    }
}
