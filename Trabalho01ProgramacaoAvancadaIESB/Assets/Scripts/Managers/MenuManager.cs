using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance = null;
    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MenuManager>();
                if (instance == null)
                {
                    GameObject managerGameObject = new GameObject("MenuManager", typeof(MenuManager));
                    instance = managerGameObject.GetComponent<MenuManager>();
                    DontDestroyOnLoad(managerGameObject);
                }
            }
            return instance;
        }

    }

    public GameObject quitCheck;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance == this)
        {
            Destroy(this.gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Race");
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
}