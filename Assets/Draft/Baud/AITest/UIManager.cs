using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class UIManager : MonoBehaviour
{
public static UIManager Instance;

[SerializeField] private TMP_Text voicelineText;
[SerializeField] private float timerVoiceline = 5f;
[SerializeField] private GameObject chaosBar;
[SerializeField] private Slider ChaosBarSlider;
[SerializeField] private Image ChaosBarSliderImageColor;
[SerializeField] private GameObject CanvasEndGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShowEndGame()
    {
        if (CanvasEndGame)
        CanvasEndGame.SetActive(true);
        gameObject.SetActive(false);
    }
    
    private void Start()
    {
        GameManager.Instance.OnIncreaseChaosBar += IncreaseChaosSlider;
        GameManager.Instance.OnDecreaseChaosBar += DecreaseChaosSlider;
    }
    
    private void IncreaseChaosSlider()
    {
        StartCoroutine(BlinkCoroutine(new UnityEngine.Color(0f, 0.78f, 0f, 1f)));
        float value = GameManager.Instance.GetChaosValueRatio();
        if (ChaosBarSlider) ChaosBarSlider.DOValue(value, 0.3f);
    }

    private void DecreaseChaosSlider()
    {
        StartCoroutine(BlinkCoroutine(new UnityEngine.Color(0.78f, 0f, 0f, 1f)));
        float value = GameManager.Instance.GetChaosValueRatio();
        if (ChaosBarSlider) ChaosBarSlider.DOValue(value, 0.3f);
    }

    private IEnumerator BlinkCoroutine(UnityEngine.Color color)
    {
        chaosBar.transform.DOShakePosition(0.5f, Vector3.right*10);
        ChaosBarSliderImageColor.DOColor(color, 0.25f);
        yield return new WaitForSeconds(0.25f);
        ChaosBarSliderImageColor.DOColor(UnityEngine.Color.white, 0.25f);
    }
    
    IEnumerator PrintVoiceline(string text)
    {
        if (voicelineText)
        voicelineText.text = text;
        yield return new WaitForSeconds(timerVoiceline);
        if (voicelineText)
        voicelineText.text = "";
    }

    public void SetVoicelineText(string text)
    {
        StartCoroutine(PrintVoiceline(text));
    }
}
