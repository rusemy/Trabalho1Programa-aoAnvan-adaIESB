using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
-comtrolar as posiçoes dos corredores
-contegem regressiva e aviso de inicio
-pause(continuar,reiniciar e sair)
*/
public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerGameObject = new GameObject("InputManager", typeof(GameManager));
                    instance = managerGameObject.GetComponent<GameManager>();
                    DontDestroyOnLoad(managerGameObject);
                }
            }
            return instance;
        }

    }

    public Transform[ ] checkPoints = new Transform[6];
    public Transform[ ] powerUpPoints0 = new Transform[5];
    public Transform[ ] powerUpPoints1 = new Transform[5];
    public Transform[ ] powerUpPoints2 = new Transform[5];
    public Transform[ ] powerUpPoints3 = new Transform[5];
    public Transform[ ] powerUpPoints4 = new Transform[5];
    public Transform[ ] powerUpPoints5 = new Transform[5];
    public Transform[, ] powerUpPoints = new Transform[6, 5];

    public int[, ] ranking = new int[3, 8];
    public int[ ] finishingRanking = new int[8];
    public int[ ] numberOfPlayerThatCompletedLap = new int[3];

    public bool isGamePaused = false;

    public GameObject pauseMenu;
    public GameObject quitCheck;
    public GameObject restartCheck;
    public GameObject startCountDown;

    public float countdownTimeToStartRace = 5f;
    public float timeToRestartRace = 10f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            powerUpPoints[0, i] = powerUpPoints0[i];
        }
        for (int i = 0; i < 5; i++)
        {
            powerUpPoints[1, i] = powerUpPoints1[i];
        }
        for (int i = 0; i < 5; i++)
        {
            powerUpPoints[2, i] = powerUpPoints2[i];
        }
        for (int i = 0; i < 5; i++)
        {
            powerUpPoints[3, i] = powerUpPoints3[i];
        }
        for (int i = 0; i < 5; i++)
        {
            powerUpPoints[4, i] = powerUpPoints4[i];
        }
        for (int i = 0; i < 5; i++)
        {
            powerUpPoints[5, i] = powerUpPoints5[i];
        }

        pauseMenu.SetActive(false);
        quitCheck.SetActive(false);
        restartCheck.SetActive(false);
    }

    private void Update()
    {
        if (ranking[2, 0] != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                finishingRanking[i] = ranking[2, i];
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void RestartCheck()
    {
        if (restartCheck.activeInHierarchy)
        {
            restartCheck.SetActive(false);
        }
        else
        {
            restartCheck.SetActive(true);
        }
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitCheck()
    {
        if (quitCheck.activeInHierarchy)
        {
            quitCheck.SetActive(false);
        }
        else
        {
            quitCheck.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator StartGameCountdown()
    {
        Time.timeScale = 0f;
        // var countdownTimer = Time.timeSinceLevelLoad + countdownTimeToStartRace;
        // while (Time.timeSinceLevelLoad < countdownTimer)
        // {
        // 	yield return 0;
        // }
        yield return new WaitForSeconds(countdownTimeToStartRace);
        Time.timeScale = 1f;
        startCountDown.SetActive(false);
    }

}