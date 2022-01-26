using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    //public LayerMask mask;
    ////public GameObject Spawn;
    //public RaycastHit[] Sphere;
    public GameObject ExplosionEffect;
    private void OnCollisionEnter(Collision collision)
    {
        //float rad = Mathf.Log(Spawn.transform.localScale.sqrMagnitude, 3)/2;
        //float rad = 1.5f;
        //Sphere = Physics.SphereCastAll(transform.position, rad, Vector3.up, 0, mask);
        //foreach (RaycastHit item in Sphere)
        //{
        //    if (item.collider.name.Contains("FireBreach"))
        //    {
        //        item.collider.GetComponent<RepairZone>().FullRepair();
        //    }
        //    if (item.collider.name.Contains("Character") && item.collider.GetComponent<FireEffect>() != null)
        //    {
        //        item.collider.GetComponent<FireEffect>().Extinguish();
        //    }
        //}
        //Instantiate(Spawn).transform.position = transform.position;
        GameObject tmp = Instantiate(ExplosionEffect);
        tmp.transform.position = gameObject.transform.position;
        tmp.transform.localScale = gameObject.transform.localScale;
        Destroy(gameObject);
    }
}
