using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnInRandomPos : MonoBehaviour
{
    public GameObject _theThing;
    public LayerMask _layerMask;
    //public bool Overlap = false;
    [Min(0)]
    public int _poolSize;
    public UnityEvent OnPoolMaxed;
    public Collider[] floors = new Collider[0];
    private GameObject[] _pool;
    //private Vector3 extents;
    private int maxLoops = 3;

    private void Start()
    {
        _pool = new GameObject[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject temp = Instantiate(_theThing);
            temp.SetActive(false);
            _pool[i] = temp;
            //temp.transform.parent = transform;
        }
    }
    public void Spawn()
    {
        // Find inactive item to enable
        GameObject obj = null;
        foreach (GameObject item in _pool)
        {
            if (!item.activeSelf)
            {
                obj = item;
                break;
            }
        }
        if (obj == null)
        {
            Debug.Log("Pool Maxed");
            OnPoolMaxed.Invoke();
            return;
        }
        // Get collider extents
        Vector3 extents = GetExtents(obj.GetComponent<Collider>());
        // Check there are floors
        if (floors.Length == 0)
        {
            Debug.LogError("Requires Floor to spawn stuff");
            return;
        }
        // Check for valid spawn
        bool validSpawn = false;
        int numLoops = 0;
        Vector3 spawnPos = Vector3.zero;
        while (!validSpawn && numLoops < maxLoops)
        {
            // Pick random floor
            int i = Random.Range(0, floors.Length);
            // Get random pos on floor
            // - may need to account for the colider centre and position offset later
            spawnPos = new Vector3(Random.Range(floors[i].bounds.min.x + extents.x, floors[i].bounds.max.x - extents.x), floors[i].bounds.max.y, Random.Range(floors[i].bounds.min.z + extents.z, floors[i].bounds.max.z - extents.z));
            validSpawn = !DetectOverlap(obj, spawnPos);
            numLoops++;
            if (numLoops == maxLoops)
            {
                Debug.LogWarning("Couldn't find space");
            }
        }
        obj.transform.position = spawnPos;
        obj.SetActive(true);

        // Spawn
        //Instantiate(_theThing).transform.position = spawnPos;
        /*
        if (!Overlap)
        {
            int numLoops = 0;
            bool overlap = true;
            while (overlap)
            {
                overlap = false;
                Collider tmp = temp.GetComponent<Collider>();
                Collider[] Hits;
                if (tmp is SphereCollider)
                {
                    //tmp = tmp as SphereCollider;
                    SphereCollider tmp1 = tmp as SphereCollider;
                    Hits = Physics.OverlapSphere(spawnPos, tmp1.radius);
                    extents = new Vector3(tmp1.radius, tmp1.radius, tmp1.radius);
                    Debug.Log("Sphere");
                }
                else
                {
                    Hits = Physics.OverlapBox(spawnPos, tmp.bounds.size);
                }
                if (Hits.Length != 0)
                {
                    spawnPos = new Vector3(Random.Range(floors[i].bounds.min.x + extents.x, floors[i].bounds.max.x - extents.x), floors[i].bounds.max.y, Random.Range(floors[i].bounds.min.z + extents.z, floors[i].bounds.max.z - extents.z));
                    overlap = true;
                    numLoops++;
                }
                //foreach (GameObject item in _pool)
                //{

                //    if (item.activeSelf && Vector3.Distance(item.transform.position, spawnPos) < extents.x * 2 * item.transform.localScale.x)
                //    {
                //        spawnPos = new Vector3(Random.Range(floors[i].bounds.min.x + extents.x, floors[i].bounds.max.x - extents.x), floors[i].bounds.max.y, Random.Range(floors[i].bounds.min.z + extents.z, floors[i].bounds.max.z - extents.z));
                //        overlap = true;
                //        numLoops++;
                //        break;
                //    }
                //}
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
        */
    }
    public Vector3 GetExtents(Collider collider)
    {
        if (collider is SphereCollider)
        {
            Debug.Log("Sphere");
            SphereCollider tmp = collider as SphereCollider;
            return new Vector3(tmp.radius, tmp.radius, tmp.radius); 
        }
        else if (collider is CapsuleCollider)
        {
            Debug.Log("Capsule");
            CapsuleCollider tmp = collider as CapsuleCollider;
            return new Vector3(tmp.radius, tmp.height, tmp.radius);
        }
        else
        {
            return collider.bounds.size;
        }
    }
    public bool DetectOverlap(GameObject item)
    {
        Collider collider = item.GetComponent<Collider>();
        Collider[] Hits;
        if (collider is SphereCollider)
        {
            //tmp = tmp as SphereCollider;
            SphereCollider tmp1 = collider as SphereCollider;
            Hits = Physics.OverlapSphere(tmp1.bounds.center, tmp1.radius, _layerMask);
            Debug.Log("Sphere");
        }
        else if(collider is CapsuleCollider)
        {
            CapsuleCollider tmp1 = collider as CapsuleCollider;
            Hits = Physics.OverlapCapsule(tmp1.bounds.min, tmp1.bounds.max, tmp1.radius, _layerMask);
            Debug.Log("Capsule");
        }
        else
        {
            Hits = Physics.OverlapBox(collider.bounds.center, collider.bounds.size);
        }
        return Hits.Length != 0;
    }
    public bool DetectOverlap(GameObject item, Vector3 position)
    {
        Collider collider = item.GetComponent<Collider>();
        Collider[] Hits;
        if (collider is SphereCollider)
        {
            //tmp = tmp as SphereCollider;
            SphereCollider tmp1 = collider as SphereCollider;
            Hits = Physics.OverlapSphere(position + tmp1.bounds.center, tmp1.radius, _layerMask);
            Debug.Log("Sphere");
        }
        else if (collider is CapsuleCollider)
        {
            CapsuleCollider tmp1 = collider as CapsuleCollider;
            Hits = Physics.OverlapCapsule(position + tmp1.bounds.min, position + tmp1.bounds.max, tmp1.radius, _layerMask);
            Debug.Log("Capsule");
        }
        else
        {
            Hits = Physics.OverlapBox(position + collider.bounds.center, collider.bounds.size);
        }
        return Hits.Length != 0;
    }
}