using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Task
{
    public class ChaosTask : MonoBehaviour
    {
        public Action<ChaosTask> OnComplete;
        public Action<ChaosTask> OnExpire;

        public bool IsActive => isActive;
        
        protected UINotif[] notifs;

        [Separator("ChaosTask")]
        public float ExpirationTime;
        public Vector3 NotifOffset;
        [SerializeField, ReadOnly] protected PlayerColor requiredColor = PlayerColor.None;

        private HashSet<PlayerController> players = new HashSet<PlayerController>();
        private bool isActive;
       
        protected void OnEnable()
        {
            isActive = true;
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
            notifs[0].SetProgressBarActive(true);
            notifs[0].PausePinFill();
            notifs[1].SetProgressBarActive(true);
            notifs[1].PausePinFill();
        }

        protected virtual void OnCancel()
        {
            notifs[0].SetProgressBarActive(false);
            notifs[0].ResumePinFill();
            notifs[1].SetProgressBarActive(false);
            notifs[1].ResumePinFill();
        }
        
        public virtual void Init()
        {
            requiredColor = GetRandomColor();
            
            notifs = UiIndicator.instance.AddObject(gameObject, requiredColor, NotifOffset.y);
            notifs[0].DoPinFill(0, ExpirationTime);
            Tween tween = notifs[1].DoPinFill(0, ExpirationTime);
            tween.onComplete += Expire;
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