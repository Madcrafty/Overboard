using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SlipperyMovement", menuName = "ScriptableObjects/MovementScript/SlipperyMovement")]
public class SlipperyMovement : Movement
{
    public float _slipSpeedMulti = 100;
    public override void Move(Rigidbody _rb, Vector3 _moveDir, float _speed)
    {
        _rb.AddForce(_moveDir * _speed * _slipSpeedMulti * Time.deltaTime, ForceMode.Force);
    }
}
