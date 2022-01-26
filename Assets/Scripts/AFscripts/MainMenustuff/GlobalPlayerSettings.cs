using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalPlayerSettings", menuName = "ScriptableObjects/Menus/GlobalPlayerSettings")]
public class GlobalPlayerSettings : ScriptableObject
{
    private float _MusicVolume, _sfxVolume;

    public void SetsfxVolume(float _newVoluyme)
    {
        _sfxVolume = _newVoluyme;
    }

    public void SetMusicVolume(float _newVolume)
    {
        _MusicVolume = _newVolume;
    }
    public float GetMusicVolume()
    {
        return _MusicVolume;
    }
    public float GetsfxVolume()
    {
        return _sfxVolume;
    }
}
