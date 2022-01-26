using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndGameset : MonoBehaviour
{
    public string _win, _lose, _bossWin, _boatSink, _boatRepaired;
    public Text _title, _summary, _reason;
    public GameObject _EndGamePanel, _startsleected;
    public EventSystem _eSystem;
    [SerializeField]
    private SceneLoadTodata _loadTo;
    [SerializeField]
    private Scene _cScene;
    [SerializeField] private InGamePanel _IGP;
    public void EnableEndGameScreen(string _id)
    {
        _eSystem.SetSelectedGameObject(_startsleected);

        if (_id == "win")
        {
            //win normal
            _EndGamePanel.SetActive(true);
            _title.text = _win;
            _IGP._mainMenuOpen = true;
            _reason.text = _boatRepaired;
            Time.timeScale = 0;

        }
        else if (_id == "lose")
        {
            //lose normal
            _EndGamePanel.SetActive(true);
            _title.text = _lose;
            _reason.text = _boatSink;
            _IGP._mainMenuOpen = true;
            Time.timeScale = 0;

        }
    }

    public void LevelRetry()
    {
        Time.timeScale = 1;
        _cScene = SceneManager.GetActiveScene();
        _loadTo._sceneIntNum = _cScene.buildIndex;
    }

    public void TimeScaleReset()
    {
        Time.timeScale = 1;
    }
}