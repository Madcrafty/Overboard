using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDifficultyScaling : DifficultyScaling
{
    public BossManager BM;
    public float[] DifficultyMod;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void UpdateDificulty(int players)
    {
        if (players >= DifficultyMod.Length)
            BM.SetDifficulty(LinearExtrapolation(DifficultyMod, players));
        else
            BM.SetDifficulty(DifficultyMod[players]);

    }
}
