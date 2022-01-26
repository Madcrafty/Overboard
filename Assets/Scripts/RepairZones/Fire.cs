using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject FireFX;
    private List<FireEffect> Players = new List<FireEffect>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.activeSelf == true)
        {
            if (other.GetComponent<FireEffect>() == null)
            {
                GameObject tmp = Instantiate(FireFX);
                tmp.transform.position = other.transform.position;
                tmp.transform.localScale = other.transform.localScale;
                tmp.transform.parent = other.transform;
                other.gameObject.AddComponent<FireEffect>().FireFX = tmp;
                other.gameObject.GetComponent<FireEffect>().inFire = true;
            }
            else
            {
                other.gameObject.GetComponent<FireEffect>().inFire = true;
            }
            Players.Add(other.gameObject.GetComponent<FireEffect>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FireEffect>().inFire = false;
            Players.Remove(other.gameObject.GetComponent<FireEffect>());
        }
    }
    public void Disable()
    {
        foreach (FireEffect item in Players)
        {
            item.inFire = false;
        }
    }
    //private void OnDestroy()
    //{
    //    foreach (FireEffect item in Players)
    //    {
    //        item.inFire = false;
    //    }
    //}
}
