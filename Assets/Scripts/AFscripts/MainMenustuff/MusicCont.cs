using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicCont : MonoBehaviour
{
    private static MusicCont _musicContInstance;
    private Scene _cScene;
    private void Awake()
    {
        if (_musicContInstance == null)
        {
            _musicContInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _cScene = SceneManager.GetActiveScene();

        if (_cScene.buildIndex == 0 || _cScene.buildIndex == 1)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(this.gameObject);
    }
}
