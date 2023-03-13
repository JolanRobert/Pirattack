using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] private GameObject TimerInfo;
    [SerializeField] private GameObject EnemyInfo;
    [SerializeField] private GameObject ScoreInfo;
    [SerializeField] private TMP_Text TimerInfoValue;
    [SerializeField] private TMP_Text EnemyInfoValue;
    [SerializeField] private TMP_Text ScoreInfoValue;
    [SerializeField] private float delay = 1.5f;
    
    IEnumerator PrintInformation()
    {
        float timer = GameManager.Instance.GetElapsedTimeInGame();
        int nbEnemiesKilled = GameManager.Instance.GetNbEnemiesKilled();
        yield return new WaitForSeconds(delay);
        TimerInfoValue.text = timer.ToString("F2");
        TimerInfo.SetActive(true);
        yield return new WaitForSeconds(delay);
        EnemyInfoValue.text = nbEnemiesKilled.ToString();
        EnemyInfo.SetActive(true);
        yield return new WaitForSeconds(delay);
        ScoreInfoValue.text = ((int)(nbEnemiesKilled * (1 + 0.1f * timer))).ToString();
        ScoreInfo.SetActive(true);
    }
    
    public void RelaunchGame(int index)
    {
        SceneManager.LoadScene(0);
    }
    
    void Start()
    {
        StartCoroutine(PrintInformation());
    }
}
