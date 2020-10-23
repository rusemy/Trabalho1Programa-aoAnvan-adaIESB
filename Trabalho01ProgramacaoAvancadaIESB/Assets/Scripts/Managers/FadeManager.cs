using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    private static FadeManager instance = null;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FadeManager>();
                if (instance == null)
                {
                    GameObject managerGameObject = new GameObject("FadeManager", typeof(FadeManager));
                    instance = managerGameObject.GetComponent<FadeManager>();
                    DontDestroyOnLoad(managerGameObject);
                }
            }
            return instance;
        }

    }

    [SerializeField] private Image fadeUI;
    [SerializeField] private float durationTime;
    private Sequence tweenSequence;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance == this)
        {
            Debug.Log("destuiiu");
            Destroy(this.gameObject);
        }

        tweenSequence = DOTween.Sequence();
        tweenSequence.Append(fadeUI.material.DOFade(0, durationTime));
        tweenSequence.Join(DOTween.To(() => fadeUI.fillAmount, fill => fadeUI.fillAmount = fill, 0, durationTime));
        tweenSequence.SetAutoKill(false);
    }
    public void FadeIn()
    {
        tweenSequence.isBackwards = false;
        tweenSequence.Restart();
    }

    public void FadeOut()
    {
        tweenSequence.PlayBackwards();
    }
}