using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private RectTransform _trackerTrans; //cursor trans
    [SerializeField]
    private float _speed = 1000f;
    [SerializeField]
    private RectTransform _canvas;
    [SerializeField]
    private Canvas _canvref;
    [SerializeField]
    private float _padding = 35f;
    [SerializeField]
    private Tracker _trackscript;
    [SerializeField]
    private bool _isMM = true;

    private Camera _maincam;
    private bool _preMouseState;
    public Mouse _virtualMouse;
    private Mouse _cMouse;
    

    private const string _gamepadScheme = "Gamepad", _mouseScheme = "Keyboard&Mouse";
    private string _previousControlScheme = "";

    private void OnEnable()
    {
        _maincam = Camera.main;
        _cMouse = Mouse.current;

        if (_virtualMouse == null)
        {
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!_virtualMouse.added)
        {
            InputSystem.AddDevice(_virtualMouse);
        }

        InputUser.PerformPairingWithDevice(_virtualMouse, _playerInput.user);

        if (_trackerTrans != null)
        {
            Vector2 position = _trackerTrans.anchoredPosition;
            InputState.Change(_virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
        _playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnDisable()
    {
        if (_virtualMouse != null && _virtualMouse.added)
        {
            InputSystem.RemoveDevice(_virtualMouse);
        }
        InputSystem.onAfterUpdate -= UpdateMotion;
        _playerInput.onControlsChanged -= OnControlsChanged;
    }

    private void UpdateMotion()
    {
        if (_virtualMouse == null || Gamepad.current == null)
        {
            return;
        }
        Vector2 _stickValue = Gamepad.current.leftStick.ReadValue();
        _stickValue *= _speed * Time.deltaTime;

        Vector2 currentposition = _virtualMouse.position.ReadValue();
        Vector2 newPosition = currentposition + _stickValue;

        newPosition.x = Mathf.Clamp(newPosition.x, _padding, Screen.width - _padding);
        newPosition.y = Mathf.Clamp(newPosition.y, _padding, Screen.height - _padding);

        InputState.Change(_virtualMouse.position, newPosition);
        InputState.Change(_virtualMouse.delta, _stickValue);

        bool _aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (_preMouseState != _aButtonIsPressed);
        {
            _virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, _aButtonIsPressed);
            InputState.Change(_virtualMouse, mouseState);
            _preMouseState = _aButtonIsPressed;
        }

        AnchorCursor(newPosition);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 _anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, position, _canvref.renderMode == RenderMode.ScreenSpaceOverlay ? null : _maincam, out _anchoredPos);
        _trackerTrans.anchoredPosition = _anchoredPos;
    }

    private void OnControlsChanged(PlayerInput _pInput)
    {
        if (_playerInput.currentControlScheme == _mouseScheme && _previousControlScheme != _mouseScheme)
        {
            _trackerTrans.gameObject.SetActive(false);
            Cursor.visible = true;
            _cMouse.WarpCursorPosition(_virtualMouse.position.ReadValue());
            if (_isMM == true)
            {
                _trackscript._controllerUsed = false;
            }
            InputState.Change(_cMouse.position, _virtualMouse.position.ReadValue());
            _previousControlScheme = _mouseScheme;
        }
        if (_playerInput.currentControlScheme == _gamepadScheme && _previousControlScheme != _gamepadScheme)
        {
            _trackerTrans.gameObject.SetActive(true);
            Cursor.visible = false;
            if (_isMM == true)
            {
                _trackscript._controllerUsed = true;
            }
            InputState.Change(_virtualMouse.position, _cMouse.position.ReadValue());
            AnchorCursor(_cMouse.position.ReadValue());
            _previousControlScheme = _gamepadScheme;
        }
    }

    private void Update()
    {
        if (_previousControlScheme != _playerInput.currentControlScheme)
        {
            OnControlsChanged(_playerInput);
        }
        _previousControlScheme = _playerInput.currentControlScheme;
    }
}
