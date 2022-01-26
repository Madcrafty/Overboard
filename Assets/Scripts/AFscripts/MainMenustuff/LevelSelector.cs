using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
   public void LoadToScene(int _loadTo)
    {
        SceneManager.LoadScene(_loadTo);
    }
}
