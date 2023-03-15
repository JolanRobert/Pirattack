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
    [SerializeField] private string dieSoundName;
    [SerializeField] private string takeDamageSoundName;

    private Dictionary<string, AudioSource> audioDictionary;
    private PlayerController[] players;

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
        if (PlayerManager.Players != null)
            players = PlayerManager.Players.ToArray();
        else
            players = new PlayerController[0];
        foreach (Sound s in audioSource)
        {
            audioDictionary.Add(s.name, s.clip);
        }

        if (players.Length > 0)
        {
            players[0].Color.OnSwitchColor += PlaySwitchSound;
            players[0].Shoot.OnShoot += PlayShootSound;
            players[0].Collision.health.OnDeath += PlayDieSound;
            players[0].Collision.health.OnHealthLose += PlayTakeDamageSound;
            
            players[1].Color.OnSwitchColor += PlaySwitchSound;
            players[1].Shoot.OnShoot += PlayShootSound;
            players[1].Collision.health.OnDeath += PlayDieSound;
            players[1].Collision.health.OnHealthLose += PlayTakeDamageSound;
        }
    }

    public void PlaySound(string name, float speed = 1f)

    {
        audioDictionary[name].Stop();
        audioDictionary[name].pitch = speed;
        audioDictionary[name].Play();
    }

    public void PlaySwitchSound()
    {
        PlaySound(switchSoundName);
    }
    
    public void PlayShootSound(WeaponData weaponData)
    {
        PlaySound(shootSoundName, weaponData.fireRate / 4);
    }
    
    public void PlayDieSound()
    {
        PlaySound(dieSoundName);
    }
    
    public void PlayTakeDamageSound()
    {
        PlaySound(takeDamageSoundName);
    }

    public void StopSound(string name)
    {
        audioDictionary[name].Stop();
    }

    private void Update()
    {
        if (players.Length > 1)
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