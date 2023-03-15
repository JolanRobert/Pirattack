using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;

[Serializable]
public struct Sound
{
    public string name;
    public AudioSource clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private Sound[] audioSource;
    [SerializeField] private string switchSoundName;
    [SerializeField] private string shootSoundName;

    private Dictionary<string, AudioSource> audioDictionary;
    PlayerController[] players;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioDictionary = new Dictionary<string, AudioSource>();
        players = PlayerManager.Players.ToArray();
        foreach (Sound s in audioSource)
        {
            audioDictionary.Add(s.name, s.clip);
        }

        if (players.Length > 1)
        {
            players[0].Color.OnSwitchColor += PlaySwitchSound;
            players[0].Shoot.OnShoot += PlayShootSound;
            
            players[1].Color.OnSwitchColor += PlaySwitchSound;
            players[1].Shoot.OnShoot += PlayShootSound;
        }
    }

    public void PlaySound(string name)
    {
        audioDictionary[name].Stop();
        audioDictionary[name].Play();
    }

    public void PlaySwitchSound()
    {
        PlaySound(switchSoundName);
    }
    
    public void PlayShootSound()
    {
        PlaySound(shootSoundName);
    }

    public void StopSound(string name)
    {
        audioDictionary[name].Stop();
    }

    private void Update()
    {
        foreach (var player in players)
        {
            if (player.MoveInput.magnitude > 0.1f && !audioDictionary["Walk"].isPlaying)
            {
                audioDictionary["Walk"].Play();
            }
            else if (player.MoveInput.magnitude < 0.1f && audioDictionary["Walk"].isPlaying)
            {
                audioDictionary["Walk"].Stop();
            }
        }
    }
}