using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingController : MonoBehaviour
{
    [Header("--Variables--")]
    [SerializeField] private bool _boatRocking;
    [SerializeField][Range(0, 5)] private float _amount;
    [SerializeField] private Transform _rotationPoint;

    [Header("--Direction--")]
    [SerializeField] [Range(0, 1)] private float x = 1;
    [SerializeField] [Range(0, 1)] private float y = 0;
    [SerializeField] [Range(0, 1)] private float z = 0;

    private void FixedUpdate()
    {
        if (_boatRocking)
        {
            float mod = Mathf.Sin(Time.time) / 50 * _amount;
            if (Time.time < Mathf.PI / 2)
                mod *= 0.5f;
            transform.RotateAround(_rotationPoint.position, Vector3.right, mod * x);
            transform.RotateAround(_rotationPoint.position, Vector3.up, mod * y);
            transform.RotateAround(_rotationPoint.position, Vector3.forward, mod * z);
        }
    }
}
