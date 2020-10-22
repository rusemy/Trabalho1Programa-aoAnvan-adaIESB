using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishingPositionUI : MonoBehaviour
{
    private TextMeshProUGUI finishingPositionText;
    private TextMeshProUGUI timeToRestartText;
    public int finishingPosition;
    private float timeToRestart;

    private void OnEnable()
    {
        finishingPosition = Array.IndexOf(GameManager.Instance.finishingRanking, 1);
        switch (finishingPosition)
        {
            case 0:
                finishingPositionText.text += "Primeiro";
                break;
            case 1:
                finishingPositionText.text += "Segundo";
                break;
            case 2:
                finishingPositionText.text += "Terceiro";
                break;
            case 3:
                finishingPositionText.text += "Quarto";
                break;
            case 4:
                finishingPositionText.text += "Quinto";
                break;
            case 5:
                finishingPositionText.text += "Sexto";
                break;
            case 6:
                finishingPositionText.text += "Sétimo";
                break;
            case 7:
                finishingPositionText.text += "Oitavo";
                break;
            default:
                finishingPositionText.text += "???";
                break;
        }
        finishingPositionText.text += " Lugar";

        timeToRestart = GameManager.Instance.timeToRestartRace;
    }

    private void Update()
    {
        timeToRestart -= Time.deltaTime;

        timeToRestartText.text = "Restarting Race in : " + Mathf.RoundToInt(timeToRestart).ToString() + " . . .";

        if (timeToRestart <= 0)
        {
            GameManager.Instance.RestartRace();
        }
    }
}