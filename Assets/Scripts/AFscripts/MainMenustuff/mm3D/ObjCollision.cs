using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//special ID to send to the collision manager
public class ObjCollision : MonoBehaviour
{
    [SerializeField] private bool _mainIsland, _secondaryIsland;
    [SerializeField] private int _colId;
    [SerializeField] private CollisionManager _manager;
    [SerializeField] private Transform _dockPos;

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("MMboat"))
        {
            if (_mainIsland != false && _secondaryIsland != true)
            {
                _manager._receivedBool = _mainIsland;
            }
            else if (_secondaryIsland != false && _mainIsland != true)
            {
                _manager._receivedBool = _secondaryIsland;
            }
            _manager._recievedId = _colId;
            _manager._moveToPos = _dockPos;
        }
    }
}
