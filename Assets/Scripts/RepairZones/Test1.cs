using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public GameObject GameObjectToHide;

    void Start()
    {
        GameObjectToHide = GameObject.FindGameObjectWithTag("GameObjectToHide");
    }

    public void HideGameObject()

    {

        if
            (GameObjectToHide.activeInHierarchy)
        {
            GameObjectToHide.SetActive(true);
        }
        else
        {
            GameObjectToHide.SetActive(false);
        }
    }
}

