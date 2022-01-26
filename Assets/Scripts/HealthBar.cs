using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public int _maxHealth;
    public Image _image;
    public RepairZone repairbase;
    private int _curHealth;
    private void Awake()
    {
        _curHealth = _maxHealth;
        UpdateCanvasSlider();
    }
    public void Damage(int damage)
    {
        _curHealth -= damage;
        UpdateCanvasSlider();
    }
    public void Repair(int repair)
    {
        _curHealth += repair;
        UpdateCanvasSlider();
    }
    public void UpdateCanvasSlider()
    {
        //slider.value = value;
        _image.fillAmount = (float)_curHealth/_maxHealth;
    }
}
