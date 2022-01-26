using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movement", menuName = "ScriptableObjects/MovementScript/Movement")]
public class Movement : ScriptableObject
{
    public virtual void Move(Rigidbody _rb, Vector3 _moveDir, float _speed)
    {

    }
}
