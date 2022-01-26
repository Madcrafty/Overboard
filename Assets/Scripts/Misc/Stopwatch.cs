using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stopwatch : MonoBehaviour
{
    [Min(0.01f)]
    public float _time = 1;
    public bool LerpTime;
    [Range(0,1)]
    public float LerpRate = 0.1f;
    [Min(0.01f)]
    public float _time2 = 0.5f;
    public UnityEvent OnTimeEnd;
    public float duration;
    private float LerpDelta;
    private float timer = 0;

    private void Start()
    {
        duration = _time;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            OnTimeEnd.Invoke();
            timer -= duration;
            if (LerpTime && duration != _time2)
            {
                LerpDelta += LerpRate*duration;
                duration = Mathf.Lerp(_time, _time2, LerpDelta);
            }
        }
    }
}