using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeColour : MonoBehaviour
{
    [SerializeField] Image _I;
    public Color _base, _new;
    private bool y;

    public void OnButtonColorChange()
    {
        y = !y;
        if (y != true)
        {
            _I.color = _base;
        }
        else
        {
            _I.color = _new;
        }

    }
}
