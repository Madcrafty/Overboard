using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Tenticles must be set as Child")]
    [Space]
    [SerializeField] private float difficulty;

    private List<GameObject> _tenticles = new List<GameObject>();
    private bool _timerActive = false;
    private int _prevPick = -1;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<BossTenticle>() != null)
            {
                _tenticles.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    private void Update()
    {
        if (!_timerActive)
        {
            float time = difficulty * Random.Range(1.0f, 3.0f);
            StartCoroutine("StartTimer", time);
        }
    }

    IEnumerator StartTimer(float waitTime)
    {
        _timerActive = true;
        yield return new WaitForSeconds(waitTime);
        int i = Random.Range(0, _tenticles.Count);
        while (i == _prevPick)
        {
            i = Random.Range(0, _tenticles.Count);
        }
        _prevPick = i;
        _tenticles[i].GetComponent<BossTenticle>().HitDeck();
        _timerActive = false;
    }
    public void SetDifficulty(float a_difficulty)
    {
        difficulty = a_difficulty;
    }
}

