using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
public static UIManager Instance;
[SerializeField] private TMP_Text voicelineText;
[SerializeField] private float timerVoiceline = 5f;

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
    
    IEnumerator PrintVoiceline(string text)
    {
        voicelineText.text = text;
        yield return new WaitForSeconds(timerVoiceline);
        voicelineText.text = "";
    }

    public void SetVoicelineText(string text)
    {
        StartCoroutine(PrintVoiceline(text));
    }
}
