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
        GameManager.Instance.OnIncreaseChaosBar += UpdateChaosSlider;
        GameManager.Instance.OnDecreaseChaosBar += UpdateChaosSlider;
        UpdateChaosSlider();
    }
    
    public void UpdateChaosSlider()
    {
        ChaosBarSliderImageColor.color = UnityEngine.Color.black;
        float value = GameManager.Instance.GetChaosValueRatio();
        if (ChaosBarSlider)
        ChaosBarSlider.DOValue(value, 0.5f);
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
