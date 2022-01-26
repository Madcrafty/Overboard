using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public int uses = 1;
    public GameObject water;
    public float YeetPower = 5;
    public void Assign(GameObject thing)
    {
        thing.GetComponent<PlayerController>().AssignInteraction(SplashWater);
    }
    public void Remove(GameObject thing)
    {
        thing.GetComponent<PlayerController>().RemoveInteraction(SplashWater);
    }
    public void SplashWater(GameObject thing)
    {
        GameObject tmp = Instantiate(water);
        tmp.transform.position = transform.position + thing.transform.forward;
        tmp.GetComponent<Rigidbody>().AddForce(thing.transform.forward * YeetPower, ForceMode.VelocityChange);
        tmp.GetComponent<Rigidbody>().angularVelocity = -thing.transform.right * 50;
        uses--;
        if (uses == 0)
        {
            thing.GetComponent<Pickup>().RemoveObject();
            //Remove(thing);
        }
    }
}
