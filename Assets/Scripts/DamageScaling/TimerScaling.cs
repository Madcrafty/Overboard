using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScaling : DifficultyScaling
{
    public Timer timer;
    public float[] TimerMods;
    public float[] LerpRate;
    public float[] Timer2Mods;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void UpdateDificulty(int players)
    {
        timer._timer = TimerMods[players];
        timer.LerpRate = LerpRate[players];
        timer._timer2 = Timer2Mods[players];
    }
}
