using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolRandomSpawn : MonoBehaviour
{
    public bool Overlap = false;
    public UnityEvent OnPoolMaxed;
    public Collider[] floors = new Collider[0];
    private Vector3 extents = new Vector3();
    private int maxLoops = 3;

    private void Start()
    {
        //Collider tmp = _theThing.GetComponent<Collider>();
        //foreach (GameObject item in transform.GetComponentsInChildren<GameObject>())
        //{
        //    if (tmp is SphereCollider)
        //    {
        //        tmp = tmp as SphereCollider;
        //        SphereCollider tmp1 = tmp as SphereCollider;
        //        extents = new Vector3(tmp1.radius, tmp1.radius, tmp1.radius);
        //        Debug.Log("Sphere");
        //    }
        //    else
        //    {
        //        extents = tmp.bounds.size;
        //    }
        //}

    }
    public void Spawn()
    {
        GameObject temp = null;
        foreach (GameObject item in transform.GetComponentsInChildren<GameObject>())
        {
            if (!item.activeSelf)
            {
                temp = item;
                break;
            }
        }
        if (temp == null)
        {
            Debug.Log("Pool Maxed");
            OnPoolMaxed.Invoke();
            return;
        }
        // Check there are floors
        if (floors.Length == 0)
        {
            Debug.LogError("Requires Floor to spawn stuff");
            return;
        }
        // Pick random floor
        int i = Random.Range(0, floors.Length);
        // Get random pos on floor
        Vector3 spawnPos = new Vector3(Random.Range(floors[i].bounds.min.x + extents.x, floors[i].bounds.max.x - extents.x), floors[i].bounds.max.y, Random.Range(floors[i].bounds.min.z + extents.z, floors[i].bounds.max.z - extents.z));
        // Spawn
        //Instantiate(_theThing).transform.position = spawnPos;
        if (!Overlap)
        {
            int numLoops = 0;
            bool overlap = true;
            while (overlap)
            {
                overlap = false;
                foreach (GameObject item in transform.GetComponentsInChildren<GameObject>())
                {
                    //Collider tc = item.GetComponent<Collider>();
                    //if (item.activeSelf && tc != null && Vector3.Distance(item.transform.position, spawnPos) < tc.extents.x * 2 * item.transform.localScale.x)
                    //{
                    //    spawnPos = new Vector3(Random.Range(floors[i].bounds.min.x + extents.x, floors[i].bounds.max.x - extents.x), floors[i].bounds.max.y, Random.Range(floors[i].bounds.min.z + extents.z, floors[i].bounds.max.z - extents.z));
                    //    overlap = true;
                    //    numLoops++;
                    //    break;
                    //}
                }
                if (numLoops >= maxLoops)
                {
                    Debug.Log("Couldn't find spot");
                    break;
                }
            }
            if (!overlap)
            {
                temp.transform.position = spawnPos;
                temp.SetActive(true);
            }
        }
        else
        {
            temp.transform.position = spawnPos;
            temp.SetActive(true);
        }
    }
}