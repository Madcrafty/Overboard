using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickRaycast : MonoBehaviour
{
    private int _rcLayerMask = 1 << 10;
    private Camera _cam;
    private void Start()
    {
        _cam = Camera.main;
    }
    public void OnClick(InputAction.CallbackContext value)
    {
        if (value.performed == true)
        {
            Ray _clickRay = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_clickRay, out RaycastHit _hit, 10000f, _rcLayerMask))
            {
                if (_hit.collider.gameObject != null)
                {
                    Debug.Log(_hit.collider.name);
                }
            }
        }
    }
}
