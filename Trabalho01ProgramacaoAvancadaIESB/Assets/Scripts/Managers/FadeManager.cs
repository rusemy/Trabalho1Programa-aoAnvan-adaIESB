using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    [SerializeField] Image fadeUI;

    public void FadeIn(float durationTime)
    {
        ShortcutExtensions.DOFI

        fadeUI.material.DOFade(0, durationTime);
    }

    public void FadeOut(float durationTime)
    {

    }
}