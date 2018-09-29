using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

	public void TriggerRestartGame()
    {
        GameManager.instance.wonOverCount = 0;
        GameManager.instance.time = 540;
        SceneManager.LoadScene("HeroChoice");
    }
}
