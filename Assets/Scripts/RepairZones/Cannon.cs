using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour
{
    public Transform ShootPoint;
    public Animator anim;
    public GameObject obj;
    public float power;
    [Space]
    [SerializeField] private float _secondsBeforeFire;
    
    public UnityEvent Fired;


    public void Fire()
    {
        StartCoroutine("FireCourutine");
    }

    IEnumerator FireCourutine()
    {
        anim.SetTrigger("Fire");

        yield return new WaitForSecondsRealtime(_secondsBeforeFire);

        GameObject tmp = Instantiate(obj);
        tmp.transform.position = ShootPoint.position;
        tmp.GetComponent<Rigidbody>().AddForce(ShootPoint.transform.forward * power, ForceMode.VelocityChange);
        //tmp.GetComponent<Rigidbody>().angularVelocity = -thing.transform.right * 50;
        Fired.Invoke();
    }
}
