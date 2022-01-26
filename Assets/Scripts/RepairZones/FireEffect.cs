using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public float duration = 5;
    public bool inFire;
    public GameObject FireFX;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.CompareTag("Player"))
        {
            transform.GetComponent<Pickup>().GrabDisabled = true;
        }
        else
        {
            Debug.Log("Panik");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inFire)
        {
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                Extinguish();
            }
        }
    }
    public void Extinguish()
    {
        transform.GetComponent<Pickup>().GrabDisabled = false;
        Destroy(FireFX);
        Destroy(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && gameObject.activeSelf == true)
        {
            if (collision.collider.GetComponent<FireEffect>() == null)
            {
                GameObject tmp = Instantiate(FireFX);
                tmp.transform.position = collision.transform.position;
                tmp.transform.localScale = collision.transform.localScale;
                tmp.transform.parent = collision.transform;
                collision.gameObject.AddComponent<FireEffect>().FireFX = tmp;
                //tmp.GetComponent<FireEffect>().inFire = true;
            }
        }
    }
}
