using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public LayerMask mask;
    public GameObject ParticleEffect;
    //public GameObject Spawn;
    public RaycastHit[] Sphere;
    private void OnCollisionEnter(Collision collision)
    {
        //float rad = Mathf.Log(Spawn.transform.localScale.sqrMagnitude, 3)/2;
        float rad = 1.5f;
        Sphere = Physics.SphereCastAll(transform.position, rad, Vector3.up, 0, mask);
        foreach (RaycastHit item in Sphere)
        {
            if (item.collider.name.Contains("FireBreach"))
            {
                item.collider.GetComponent<RepairZone>().FullRepair();
            }
            if (item.collider.name.Contains("Character") && item.collider.GetComponent<FireEffect>() != null)
            {
                item.collider.GetComponent<FireEffect>().Extinguish();
            }
        }
        StartCoroutine("Paricles");
        Destroy(gameObject);
    }
    IEnumerator Paricles()
    {
        GameObject _tmp = Instantiate(ParticleEffect);
        _tmp.transform.position = transform.position;
        yield return new WaitForSecondsRealtime(ParticleEffect.GetComponent<ParticleSystem>().duration);
        Destroy(_tmp);
    }
}
