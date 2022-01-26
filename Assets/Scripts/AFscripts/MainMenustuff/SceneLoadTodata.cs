using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/Menus/SceneData")]
public class SceneLoadTodata : ScriptableObject
{

    public int _sceneIntNum;

    public void ChangeValue(int _num)
    {
        _sceneIntNum = _num;
    }

}
