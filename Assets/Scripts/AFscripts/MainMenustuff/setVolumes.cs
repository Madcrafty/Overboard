using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setVolumes : MonoBehaviour
{
    [SerializeField]
    private GlobalPlayerSettings _ref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusicVolume(AudioSource _Source)
    {
        _Source.volume = _ref.GetMusicVolume();
    }
    public void SetsfxVolume(AudioSource _source)
    {
        _source.volume = _ref.GetsfxVolume();
    }
}
