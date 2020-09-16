using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-comtrolar as posiçoes dos corredores
-contegem regressiva e aviso de inicio
-pause(continuar,reiniciar e sair)
*/
public class GameManager : MonoBehaviour
{
    

    private static GameManager instance = null;
    public static GameManager Instance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if(instance == null)
                {
                    GameObject managerGameObject = new GameObject("InputManager", typeof(GameManager));
                    instance = managerGameObject.GetComponent<GameManager>();
                    DontDestroyOnLoad(managerGameObject);
                }
            }
            return instance;
        }
    }

    public Transform[] checkPoints = new Transform[6];

    public Transform[] powerUpPoints0 = new Transform[5];
    public Transform[] powerUpPoints1 = new Transform[5];
    public Transform[] powerUpPoints2 = new Transform[5];
    public Transform[] powerUpPoints3 = new Transform[5];
    public Transform[] powerUpPoints4 = new Transform[5];
    public Transform[] powerUpPoints5 = new Transform[5];

    public Transform[ , ] powerUpPoints = new Transform[6,5];

    public int[,] ranking = new int[3,8];
    public int[] numberOfPlayerThatCompletedLap = new int[3]; 

    private void Awake() {
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

    }

    
}
