using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    public RectTransform _trackerObj;
    private bool _unclicked = true;
    public MenuShift _sendTo;
    // Update is called once per frame
    void Update()
    {
        if (_unclicked == true)
            _trackerObj.position = Input.mousePosition;
        else
            _trackerObj.position = _trackerObj.position;
    }

    public void ClickedDestination(Transform y)
    {
        _unclicked = false;
        _trackerObj.position = y.position;
    }
    public void NextPanelTo(int _screenTo)
    {
        _sendTo._pId = _screenTo;
    }
    public void FollowMouseAgain()
    {
        _unclicked = true;
    }
}
