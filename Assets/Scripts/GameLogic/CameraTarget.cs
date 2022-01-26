using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private List<GameObject> Players = new List<GameObject>();
    [Space]
    [SerializeField] private Vector3 _centerAnchor = Vector3.zero;
    [SerializeField] private float _centeringStrength = 1;

    void FixedUpdate()
    {
        List<Vector3> poss = new List<Vector3>();
        for (int i = 0; i < _centeringStrength; i++)
            poss.Add(_centerAnchor);

        foreach (var player in Players)
        {
            poss.Add(player.transform.position);
            Vector3 camPPos = Camera.main.WorldToScreenPoint(player.transform.position);
        }

        Vector3 relativePos = GetAverageVector(poss) - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
    }

    private Vector3 GetAverageVector(List<Vector3> positions)
    {
        if (positions.Count == 0)
            return Vector3.zero;

        float x = 0f; 
        float y = 0f; 
        float z = 0f;

        foreach (Vector3 pos in positions)
        {
            x += pos.x; 
            y += pos.y; 
            z += pos.z;
        }
        return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
    }

    public void AddPlayer()
    {
        GameObject[] newPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in newPlayers)
            Players.Add(player);
    }
}
