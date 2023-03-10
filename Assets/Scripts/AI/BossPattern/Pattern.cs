using Player;
using UnityEngine;

namespace AI.BossPattern
{
    public abstract class Pattern : ScriptableObject
    {
        public Boss caster;

        public abstract float GetDelay();
    
        public abstract void TouchPlayer(PlayerController player); // Called when the boss touch the player
    
        public abstract void EndTrigger(GameObject obj); // Called when the boss stop touching the player

        public abstract void Execute();
    }
}