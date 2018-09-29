using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour {

    public GameObject _menu;
    public bool _visibleAtStart = false;

    private bool _isVisible;

	// Use this for initialization
	void Start () {
        _menu.SetActive(_visibleAtStart);
        _isVisible = _visibleAtStart;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menu != null) {
                TriggerOpenMenu();
            }
        }
    }

    public void TriggerOpenMenu()
    {
        if (!_isVisible)
        {
            _menu.SetActive(true);
            _isVisible = true;
            if(GameManager.instance != null)
            {
                GameManager.instance.gameActive = false;
            }
            
        }
        else
        {
            _menu.SetActive(false);
            _isVisible = false;
            if (GameManager.instance != null)
            {
                GameManager.instance.gameActive = true;
            }
        }

    }

    public void TriggerRestartGame()
    {
        if (GameManager.instance != null) {
            GameManager.instance.InitGame();
        }
        TriggerOpenMenu();
        SceneManager.LoadScene("HeroChoice");
    }

    public void TriggerQuitGame()
    {
        Application.Quit();
    }
}
