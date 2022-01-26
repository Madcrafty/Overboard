using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    [Header("--Variables--")]
    [SerializeField] private SO_PlayerIDArray _characterData;
    [SerializeField] private List<GameObject> _charPanels = new List<GameObject>();
    [SerializeField] [Range(0, 3)] private int _panelNumber;
    [Header("--Game Events--")]
    [SerializeField] private GameEvent _playerReady;
    [SerializeField] private GameEvent _playerNotReady;

    private int _i = 0; // iterator
    private int _iPrev = 0;
    private bool _ready = false;

    private void Awake()
    {
        _i = _panelNumber;
        _iPrev = _i;
        HideAll();
        _charPanels[_i].SetActive(true);
    }

    public void NextCharacter()
    {
        if (_characterData.AvailableCount() > 1 && !_ready)
        {
            while (_i == _iPrev || _characterData._inputIDbyPlayers[_i].charAvailable == false)
            {
                _i++;
                if (_i > 3)
                    _i = 0;
                if (_i == _iPrev)
                    break;
            }
            ControlledUpdate();
        }
    }

    public void PrevCharacter()
    {
        if (_characterData.AvailableCount() > 1 && !_ready)
        {
            while (_i == _iPrev || _characterData._inputIDbyPlayers[_i].charAvailable == false)
            {
                _i--;
                if (_i < 0)
                    _i = 3;
                if (_i == _iPrev)
                    break;
            }
            ControlledUpdate();
        }
    }

    public void ReadyCharacter()
    {
        if (!_ready)
        {
            _characterData._inputIDbyPlayers[_i].player = _panelNumber;
            _characterData._inputIDbyPlayers[_i].charAvailable = false;
            _ready = true;
            _playerReady.Invoke();
        }
        else
        {
            _characterData._inputIDbyPlayers[_i].player = -1;
            _characterData._inputIDbyPlayers[_i].charAvailable = true;
            _ready = false;
            _playerNotReady.Invoke();
        }
    }

    private void ControlledUpdate()
    {
        _iPrev = _i;
        HideAll();
        _charPanels[_i].SetActive(true);
    }

    public void OnReadyPlayer()
    {
        if (_characterData._inputIDbyPlayers[_i].charAvailable == false && !_ready)
        {
            while (_i == _iPrev || _characterData._inputIDbyPlayers[_i].charAvailable == false)
            {
                _i++;
                if (_i > 3)
                    _i = 0;
                if (_i == _iPrev)
                    break;
            }
            ControlledUpdate();
        }
    }

    public void OnNotReadyPlayer()
    {

    }

    private void HideAll()
    {
        if (!_ready)
            foreach (GameObject obj in _charPanels)
                obj.SetActive(false);
    }
}
