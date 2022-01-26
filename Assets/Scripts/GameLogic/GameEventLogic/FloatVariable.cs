using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public float InitialValue;

	[NonSerialized]
	public float RuntimeValue;

	public void OnAfterDeserialize()
	{
		RuntimeValue = InitialValue;
	}

	public void OnBeforeSerialize() { }

	public void Subtract(float amount) { RuntimeValue -= amount; }

	public void Add(float amount) { RuntimeValue += amount; }

	public void Set(float amount) { RuntimeValue = amount; }
}
