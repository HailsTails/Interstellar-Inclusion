using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public int time;
    public GameObject player;
    public bool gameActive;

    public AlienType playerType;
    public int wonOverCount = 0;

    public Alien AlienX;
    public Alien AlienY;
    public Alien AlienYV;
    public Alien AlienZ;

    public bool gameOver = false;

    void Awake() {
		if (instance == null) {
			instance = this;
		}

		else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		InitGame();
	}

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            InitGame();

            GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
            foreach (GameObject alien in aliens)
            {
                Alien alienScript = alien.GetComponent<Alien>();
                if (alienScript.type == AlienType.X)
                {
                    AlienX = alienScript;
                }
                else if (alienScript.type == AlienType.Y)
                {
                    AlienY = alienScript;
                }
                else if (alienScript.type == AlienType.Z)
                {
                    AlienZ = alienScript;
                }
                else if (alienScript.type == AlienType.V)
                {
                    AlienYV = alienScript;
                }
            }
        }
    }

    void Start() {
		InvokeRepeating("passTime", 0.0f, 2.0f);
    }

    void FixedUpdate() {
        if (time >= 1020 && !gameOver) {
            EndGame();
        }
        if (wonOverCount >= 3 && !gameOver)
        {
            EndGame();
        }
        if (AlienX != null && AlienY != null && AlienYV != null && AlienZ != null) { 
            if (AlienX.conversed && AlienY.conversed && AlienYV.conversed && AlienZ.conversed && !gameOver)
            {
                EndGame();
            }
        }
	}

	public void InitGame() {
		time = 540;
        gameActive = true;
        gameOver = false;
        wonOverCount = 0;
    }

	void passTime() {
        if (gameActive)
        {
            time += 15;
        }
	}

    public int getHour()
    {
        return this.time/60;
    }

    public int getMinute()
    {
        return this.time%60;
    }

    public void EndGame()
    {
        Debug.Log("dialogue over");

        gameOver = true;

        if (wonOverCount >= 3) //will need success criteria here
        {
            SceneManager.LoadScene("success");
        } else {
            SceneManager.LoadScene("failure");
        }
    }

}
