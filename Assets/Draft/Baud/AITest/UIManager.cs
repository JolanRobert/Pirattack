using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
public static UIManager Instance;

[SerializeField] private TMP_Text voicelineText;
[SerializeField] private float timerVoiceline = 5f;
[SerializeField] private Slider ChaosBarSlider;
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
        GameManager.OnIncreaseChaosBar += UpdateChaosSlider;
        GameManager.OnDecreaseChaosBar += UpdateChaosSlider;
        UpdateChaosSlider();
    }
    
    public void UpdateChaosSlider()
    {
        float value = GameManager.Instance.GetChaosValueRatio();
        if (ChaosBarSlider)
        ChaosBarSlider.DOValue(value, 0.5f);
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
