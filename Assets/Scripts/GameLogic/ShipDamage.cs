using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShipDamage : MonoBehaviour
{
    [Header("--Game Events--")]
    [SerializeField] private GameEvent _onLoose;

    [Header("--Variables--")]
    [SerializeField] [Tooltip("Total health for the ship")] private float _maxHealth = 100;
    [SerializeField] [Tooltip("Damage dealt per tenticle sla[")] private float _damageOnTenticleHit = 10;
    [Space]
    [SerializeField] private float _timeTakenToDamage = 1;
    [SerializeField] private float _damagePerRepairZone = 1;
    [SerializeField] private float _repairPerRepairZone = 1;

    [Header("--Rules--")]
    [SerializeField] [Tooltip("True for Time, False for Damage")] private bool TimeOrDamage = false;
    [SerializeField] [Tooltip("Enable both progressive damage and boss damage")] private bool Both = false;

    [Header("--Health Bar--")]
    [SerializeField] private Image _healthBar;

    private bool[] _breachTracker = new bool[6];
    private float _health;
    private int _repairZones = 0;
    private bool _lost = false;

    private void Start()
    {
        for (int i = 0; i < _breachTracker.Length; i++)
            _breachTracker[i] = true;

        _health = _maxHealth;
        StartCoroutine("MainLoop");
        _healthBar.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (_health <= 0 && !_lost)
        {
            _onLoose.Invoke();
            _lost = true;
        }

        HealthBarUpdate();
    }

    IEnumerator MainLoop()
    {
        while (!TimeOrDamage || Both)
        {
            yield return new WaitForSecondsRealtime(_timeTakenToDamage);
            _health -= _damagePerRepairZone * _repairZones;
        }
    }

    public void AddRepairZone()
    {
        _repairZones++;
    }

    public float GetHealth()
    {
        return _health;
    }
    public void SetMaxHealth(float a_maxhp)
    {
        _maxHealth = a_maxhp;
    }
    public void SetTenticleDamage(float a_damageOnTenticleHit)
    {
        _damageOnTenticleHit = a_damageOnTenticleHit;
    }
    public void SetDamageTick(float a_timeTakenToDamage)
    {
        _timeTakenToDamage = a_timeTakenToDamage;
    }
    public void SetDamagePerZone(float a_damagePerRepairZone)
    {
        _damagePerRepairZone = a_damagePerRepairZone;
    }
    public void SetRepairPerZone(float a_repairPerRepairZone)
    {
        _repairPerRepairZone = a_repairPerRepairZone;
    }

    private void HealthBarUpdate()
    {
        _healthBar.GetComponent<Image>();
        _healthBar.fillAmount = _health / _maxHealth;
    }

    // repair with default amount
    public void RepairShip()
    {
        if (TimeOrDamage || Both)
        {
            _health += _repairPerRepairZone * _repairZones;
            _repairZones--;
        }
        if (_health > _maxHealth)
            _health = 100;
    }

    // repair from boss damage
    public void BossRepair(int breachNum)
    {
        if (!TimeOrDamage || Both)
        {
            _health += _damageOnTenticleHit;
            _breachTracker[breachNum - 1] = true;
        }
        if (_health > _maxHealth)
            _health = 100;
    }

    // damaging ship for boss amount
    public void BossDamage(int breachNum)
    {
        if (!TimeOrDamage || Both)
        {
            if (_breachTracker[breachNum - 1])
            {
                _health -= _damageOnTenticleHit;
                _breachTracker[breachNum - 1] = false;
            }
        }
    }
}
