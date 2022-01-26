using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(TextMeshPro))]
public class AlterFloatVariable : MonoBehaviour
{
    [SerializeField] FloatVariable _floatVariable;
    [SerializeField] TextMeshProUGUI _text;

    private void Update()
    {
        _text.text = _floatVariable.RuntimeValue.ToString();
    }
}
