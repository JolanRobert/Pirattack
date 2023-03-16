using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        public UI_Lobby uiLobby;
        public List<PlayerInput> players = new();
        public List<InputDevice> devices = new();

        private void Awake()
        {
            InitializeSingleton(false);
        }

        public void InstantiatePlayers()
        {
            foreach (var gamepad in Gamepad.all)
            {
                AddPlayer(gamepad);
            }
        }
    
        public void ClearPlayers()
        {
            foreach (var player in players)
            {
                Destroy(player.gameObject);
            }
            devices.Clear();
            players.Clear();
        }

        public void AddPlayer(InputDevice device)
        {
            if (device is not Gamepad || IsDeviceAlreadyUsed(device)) return;
            players.Add(PlayerInputManager.instance.JoinPlayer(controlScheme: "Gamepad", pairWithDevice: device));
            devices.Add(device);
        }

        private bool IsDeviceAlreadyUsed(InputDevice device)
        {
            return players.Any(player => player.devices.ToList().Contains(device)) || devices.Contains(device);
        }
    }
}
