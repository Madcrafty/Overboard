using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private int _sceneTo;
    [SerializeField] private SceneLoadTodata _index;
    [SerializeField] private GameObject tohide, _finishText;
    [SerializeField] private PlayerInput _i;

    private bool _finishLoading = false;
    private int _loadingSceneIndex = 0;

    void Start()
    {
        _loadingSceneIndex = SceneManager.GetSceneByName("LoadingScene").buildIndex;
        _finishLoading = false;
        _sceneTo = _index._sceneIntNum;
        StartCoroutine(LoadAsyncOperation());
    }

    public void OnSubmit(InputAction.CallbackContext value)
    {
        _finishLoading = true;
        Debug.Log("pressed");
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(_sceneTo, LoadSceneMode.Single);
        gamelevel.allowSceneActivation = false;
        while (!gamelevel.isDone)
        {
            _progressBar.fillAmount = gamelevel.progress;
            if (gamelevel.progress >= 0.9f)
            {
                _progressBar.fillAmount = 1;
                _finishText.SetActive(true);
                if (_finishLoading == true)
                {
                    //SceneManager.UnloadSceneAsync(_loadingSceneIndex);
                    gamelevel.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}