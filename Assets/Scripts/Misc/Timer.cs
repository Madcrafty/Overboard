using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [Min(0.01f)]
    public float _timer = 1;
    [Tooltip("Reset timer when timer is finished")]
    //public bool autoReset;
    public bool LerpTime;
    [Range(0, 1)]
    public float LerpRate = 0.1f;
    [Min(0.01f)]
    public float _timer2 = 0.5f;
    public TextMeshProUGUI OutputUI;
    public UnityEvent OnTimerComplete; 
    public float curTimer;
    private float LerpDelta;
    private float timer = 0;

    private void Start()
    {
        curTimer = _timer;
        timer = curTimer;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (OutputUI != null)
        {
            int seconds = (int)timer % 60;
            int minutes = (int)(timer / 60) % 60;
            OutputUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (timer <= 0)
        {
            OnTimerComplete.Invoke();
            if (LerpTime && curTimer != _timer2)
            {
                LerpDelta += LerpRate * curTimer;
                curTimer = Mathf.Lerp(_timer, _timer2, LerpDelta);
                timer += curTimer;
            }
            else
            {
                enabled = false;
            }
        }
    }
    public void ResetTimer()
    {
        timer += curTimer;
        LerpDelta = 0;
        enabled = true;
    }
}
