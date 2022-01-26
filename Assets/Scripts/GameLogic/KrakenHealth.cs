using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KrakenHealth : MonoBehaviour
{
    [Header("--Game Events--")]
    [SerializeField] private GameEvent _onKrakenDie;

    [Header("--Variabels--")]
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _damagePerShot = 10;

    [Header("--Health Bar--")]
    [SerializeField] private Image _healthBar;

    private float _health;

    private void Start()
    {
        Time.timeScale = 1;
        _health = _maxHealth;
        _healthBar.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        _healthBar.fillAmount = _health / _maxHealth;
        if (_health <= 0)
        {
            _onKrakenDie.Invoke(); 
            Time.timeScale = 0;
        }
    }

    public void DamageKraken()
    {
        _health -= _damagePerShot;
    }
}
