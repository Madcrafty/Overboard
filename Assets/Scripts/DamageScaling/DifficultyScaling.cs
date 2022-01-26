using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public virtual void UpdateDificulty(int players)
    {

    }
    public virtual float LinearExtrapolation(float[] list, int difficulty)
    {
        float stepAmount = (list[list.Length - 1] - list[0]) / list.Length;
        return stepAmount * difficulty + list[0];
    }
}
