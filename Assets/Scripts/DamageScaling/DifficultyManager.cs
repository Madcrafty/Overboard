using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public List<DifficultyScaling> difficultyScaling;
    private PlayerInputManager PM;

    private void Start()
    {
        PM = GetComponent<PlayerInputManager>();
    }
    public void UpdateScalers()
    {
        StartCoroutine(WaitForPIMUpdate());
    }
    public IEnumerator WaitForPIMUpdate()
    {
        yield return new WaitForEndOfFrame();
        int playerCount = PM.playerCount;
        if (playerCount == 0)
        {
            Debug.LogWarning("Can not update dificulty for zero players");
        }
        else
        {
            foreach (DifficultyScaling item in difficultyScaling)
            {
                item.UpdateDificulty(playerCount - 1);
            }
        }

    }
}
