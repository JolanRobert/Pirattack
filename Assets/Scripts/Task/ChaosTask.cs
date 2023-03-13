using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Task
{
    public class ChaosTask : MonoBehaviour
    {
        public Action<ChaosTask> OnComplete;
        public Action<ChaosTask> OnExpire;

        public float ProgressAmount => taskInfos.GetProgressAmount();
        public bool IsActive => isActive;

        [Separator("ChaosTask")]
        [SerializeField] protected TaskInfos taskInfos;
        [SerializeField] private float expirationTime;
        [SerializeField, ReadOnly] protected PlayerColor requiredColor = PlayerColor.None;

        private HashSet<PlayerController> players = new HashSet<PlayerController>();
        private bool isActive;

        private Image[] notifs;
        
        protected void OnEnable()
        {
            isActive = true;

            taskInfos.SetProgressFill(0);
            taskInfos.SetPinFill(1);
            taskInfos.Show(TaskInfos.InfoGroup.Pin);
            Tween tween = taskInfos.DoPinFill(0, expirationTime);
            tween.onComplete += Expire;
        }
            
        protected virtual void OnDisable()
        {
            isActive = false;
                    
            foreach (PlayerController player in players)
            {
                player.Interact.Unsubscribe(this);
                player.Interact.OnBeginInteract -= OnBegin;
                player.Interact.OnEndInteract -= OnCancel;
            }
                
            players.Clear();
        }

        protected virtual void OnBegin()
        {
            taskInfos.Show(TaskInfos.InfoGroup.Progress);
            taskInfos.PausePinFill(notifs);
        }

        protected virtual void OnCancel()
        {
            taskInfos.Show(TaskInfos.InfoGroup.Pin);
            taskInfos.ResumePinFill(notifs);
        }
        
        public virtual void Init()
        {
            requiredColor = GetRandomColor();
            taskInfos.SetPinColor(requiredColor);
            notifs = UiIndicator.instance.AddObject(gameObject, requiredColor);
            notifs[0].DOFillAmount(0, expirationTime);
            notifs[1].DOFillAmount(0, expirationTime);
        }

        public bool IsValid()
        {
            foreach (PlayerController player in players)
            {
                if (requiredColor == PlayerColor.None || player.PColor == requiredColor) return true;
            }

            return false;
        }
        
        protected PlayerColor GetRandomColor()
        {
            List<PlayerColor> colors = Enum.GetValues(typeof(PlayerColor)).Cast<PlayerColor>().ToList();
            colors.Remove(PlayerColor.None);
            
            return colors[Random.Range(0, colors.Count)];
        }

        private void Expire()
        {
            OnExpire.Invoke(this);
        }
        
        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Add(player);
                player.Interact.Subscribe(this);
                player.Interact.OnBeginInteract += OnBegin;
                player.Interact.OnEndInteract += OnCancel;
            }
        }
        
        protected void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                players.Remove(player);
                player.Interact.Unsubscribe(this);
                player.Interact.OnBeginInteract -= OnBegin;
                player.Interact.OnEndInteract -= OnCancel;
            }
        }
    }
}