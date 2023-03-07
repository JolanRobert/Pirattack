using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
public static UIManager Instance;
[SerializeField] private TMP_Text voicelineText;
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
    
    
    
    public void SetVoicelineText(string text)
    {
        voicelineText.text = text;
    }
}
