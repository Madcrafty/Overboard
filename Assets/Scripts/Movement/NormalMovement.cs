using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NormalMovement", menuName = "ScriptableObjects/MovementScript/NormalMovement")]
public class NormalMovement : Movement
{
    public override void Move(Rigidbody _rb, Vector3 _moveDir, float _speed)
    {
        _rb.MovePosition(_rb.transform.position + (_moveDir * _speed * Time.deltaTime));
    }
}
