using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour
{
    public Transform _toLookAt;
    public Transform _objLooking;
    public bool _inGame;
    public bool _canLook;

    // Update is called once per frame
    private void Start()
    {
        if (_inGame == true)
        {
            _toLookAt = Camera.main.transform;
        }
    }
    void Update()
    {
        if (_canLook == true)
        {
            _objLooking.transform.LookAt(_toLookAt, Vector3.up);
        }
    }
}
/*
public class MiscCOllide :MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayAnimationClip();
    }

    void PlayANimationClips()
    {
        //do animations

        //now reset
        BoatLogic.Respawn();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}*/