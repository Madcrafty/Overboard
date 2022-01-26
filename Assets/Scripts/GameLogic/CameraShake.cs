using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeAmount;
    [SerializeField] private float _shakeTime;
    [SerializeField] private int _jitterGap;

    private Quaternion _originalRot;

    public void ShakeTheCamera()
    {
        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine()
    {
        float time = 0;
        int i = 0;
        _originalRot = transform.rotation;

        while (time <= _shakeTime)
        {
            i++;
            if (i >= _jitterGap)
            {
                i = 0;

                transform.rotation = new Quaternion(_originalRot.x + Random.Range(-_shakeAmount / 100, _shakeAmount / 100),
                                                    _originalRot.y + Random.Range(-_shakeAmount / 100, _shakeAmount / 100),
                                                    _originalRot.z + Random.Range(-_shakeAmount / 100, _shakeAmount / 100),
                                                    _originalRot.w + Random.Range(-_shakeAmount / 100, _shakeAmount / 100));
            }

            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = _originalRot;
    }
}
