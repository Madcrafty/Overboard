using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setslidervalue : MonoBehaviour
{
    [SerializeField]
    private Slider _musicSlider, _sfxSlider;
    [SerializeField]
    private GlobalPlayerSettings _ref;
    // Start is called before the first frame update
    void Start()
    {
        _musicSlider.value = _ref.GetMusicVolume();
        _sfxSlider.value = _ref.GetsfxVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
