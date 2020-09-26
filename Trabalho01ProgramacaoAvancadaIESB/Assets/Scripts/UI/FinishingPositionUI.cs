using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishingPositionUI : MonoBehaviour
{
    private TextMeshProUGUI finishingPositionText;
    public int finishingPosition;

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

    }

    IEnumerator RestartRaceAfterXTime()
    {
        yield return new WaitForSeconds(GameManager.Instance.timeToRestartRace);
        GameManager.Instance.RestartRace();
    }
}