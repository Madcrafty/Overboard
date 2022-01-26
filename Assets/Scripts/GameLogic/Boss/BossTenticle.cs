using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossTenticle : MonoBehaviour
{
    [SerializeField] private UnityEvent onHit;
    [SerializeField] [Range(1, 6)] [Tooltip("Must be corrospond with the tenticle number")] private int tenticleNum;
    [SerializeField] private GameObject holeBreachObject;
    //[SerializeField] private GameObject RepairedPlanks;
    //[SerializeField] private GameObject PlanksExplodingParticle;

    private Animator _animator;

    private void Start()
    {
        holeBreachObject.SetActive(false);
        _animator = GetComponent<Animator>();
        //RepairedPlanks.SetActive(true);
    }

    private void Update()
    {
        _animator.ResetTrigger("Hit");
    }

    public void HitDeck()
    {
        _animator.SetTrigger("Hit");
        StartCoroutine("Breach");
    }

    IEnumerator Breach()
    {
        yield return new WaitForSeconds(0.8f);
        holeBreachObject.SetActive(true);
        onHit.Invoke();
        //RepairedPlanks.SetActive(false);
        //GameObject _PlanksExplodingParticle = Instantiate(PlanksExplodingParticle);
        //Destroy(_PlanksExplodingParticle, 3);

    }   
}