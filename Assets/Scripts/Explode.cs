using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explode : MonoBehaviour
{
    public GameObject exp;
    public float expForce, radius;
    private Rigidbody rb;

    private void OnCollisionEnter(Collision other)
    {
        GameObject _exp = Instantiate(exp, transform.position, transform.rotation);
        Destroy(_exp, 3);
        KnockBack();
        Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 100
            );
    }
    void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearyby in colliders)
        {
           Rigidbody rigg =nearyby.GetComponent<Rigidbody>();
            if(rigg !=null)
            {
                rigg.AddExplosionForce(expForce, transform.position, radius);
            }
        }

  
    }

}
