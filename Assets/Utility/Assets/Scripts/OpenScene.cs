using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour {

    public string _sceneName = "GameScene";
    public AlienType type;

    public void TriggerOpenScene()
    {
        GameManager.instance.playerType = type;
        SceneManager.LoadScene(_sceneName);
    }
}
