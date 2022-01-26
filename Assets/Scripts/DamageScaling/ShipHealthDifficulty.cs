using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthDifficulty : DifficultyScaling
{
    public ShipDamage SD;
    public float[] MaxHealthMod;
    public float[] DamageOnTenticleHitMod;
    public float[] TimeTakenToDamageMod;
    public float[] DamagePerRepairZoneMod;
    public float[] RepairPerRepairZoneMod;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void UpdateDificulty(int players)
    {
        if (players >= MaxHealthMod.Length)
            SD.SetMaxHealth(LinearExtrapolation(MaxHealthMod, players));
        else
            SD.SetMaxHealth(MaxHealthMod[players]);

        if (players >= DamageOnTenticleHitMod.Length)
            SD.SetTenticleDamage(LinearExtrapolation(DamageOnTenticleHitMod, players));
        else
            SD.SetTenticleDamage(DamageOnTenticleHitMod[players]);

        if (players >= TimeTakenToDamageMod.Length)
            SD.SetTenticleDamage(LinearExtrapolation(TimeTakenToDamageMod, players));
        else
            SD.SetDamageTick(TimeTakenToDamageMod[players]);

        if (players >= DamagePerRepairZoneMod.Length)
            SD.SetTenticleDamage(LinearExtrapolation(DamagePerRepairZoneMod, players));
        else
            SD.SetDamagePerZone(DamagePerRepairZoneMod[players]);

        if (players >= RepairPerRepairZoneMod.Length)
            SD.SetTenticleDamage(LinearExtrapolation(RepairPerRepairZoneMod, players));
        else
            SD.SetRepairPerZone(RepairPerRepairZoneMod[players]);
    }
}
