﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-comtrolar as posiçoes dos corredores
-contegem regressiva e aviso de inicio
-pause(continuar,reiniciar e sair)
*/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
