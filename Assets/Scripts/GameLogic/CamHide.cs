using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHide : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private float targetAlpha;

    private float _initialAlpha;
    public MeshRenderer[] _meshRenderer = new MeshRenderer[0];

    private void Start()
    {
        _meshRenderer = GetComponentsInChildren<MeshRenderer>();

        _initialAlpha = 100;
    }

    void FixedUpdate()
    {


        foreach (GameObject player in players)
        {
            Vector3 offset = new Vector3(0, 2, 0);
            RaycastHit hit;
            float dist = Vector3.Distance(camera.transform.position + offset, player.transform.position);
            Vector3 fwd = (camera.transform.position + offset) - player.transform.position;

            bool interstect = false;

            if (Physics.Raycast(player.transform.position + offset, fwd, out hit, dist))
                interstect = true;

            foreach (MeshRenderer renderer in _meshRenderer)
            {
                Color newColor = renderer.material.color;
                if (interstect)
                {
                    newColor.a = targetAlpha / 100;
                }
                else
                    newColor.a = _initialAlpha / 100;
                renderer.material.color = newColor;
            }
        }
    }

    public void AddPlayer()
    {
        var objects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in objects)
        {
            if (!players.Contains(obj))
            {
                players.Add(obj);
            }
        }
    }
}
