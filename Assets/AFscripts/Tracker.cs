using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    public RectTransform _trackerObj;

    // Update is called once per frame
    void Update()
    {
        _trackerObj.position = Input.mousePosition;
    }
}
