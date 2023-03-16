using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;
using Utils;

namespace AI.BossPattern
{
    [CreateAssetMenu(fileName = "BottleHarassment", menuName = "ScriptableObjects/Pattern/BottleHarassment", order = 1)]
    public class BottleHarrasment : Pattern
    {
        [SerializeField] private GameObject bottleAnimationPrefab;

        public override float GetDelay()
        {
            BossData data = caster.data;
            float animationTime = ((55 / Time.deltaTime * 45f) * Time.deltaTime) / 1000f;
            float animationBottle = 3f; // 3f = temps de l'animation up
            float animationFallBottle = data.nbBottleHarassment * data.delayBetweenBottleHarassment + animationTime;
            return animationBottle + data.delayBeforeFallingHarassment + animationFallBottle;
        }

        public override void TouchPlayer(PlayerController player)
        {
            player.Collision.Damage(caster.data.damagePerBottleHarassment);
            Debug.Log("Touch Player by BottleRain");
        }

        public override void EndTrigger(GameObject obj)
        {
            obj.GetComponent<BottleFalling>().EndOfLife();
            Pooler.Instance.Depop(Pooler.Key.Bottle, obj);
        }

        IEnumerator ExecuteBottleHarassmentAnimation()
        {
            BossData data = caster.data;
            caster.Animator.SetTrigger("ThrowBottle");
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 4; i++)
            {
                Instantiate(bottleAnimationPrefab, caster.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(data.delayBeforeFallingHarassment);
        
            List<float> randomPosList = new List<float>();
            for (float j = 0; j < data.probabilityDistanceCurve.length; j += 0.1f)
            {
                float nbtime = data.probabilityDistanceCurve.Evaluate(j);
                for (int k = 0; k < nbtime; k++)
                {
                    randomPosList.Add(j);
                    randomPosList.Add(-j);
                }
            } 
        
            for (int i = 0; i < data.nbBottleHarassment; i++)
            {
                Vector3 playerPos = PlayerManager.Players[i % 2].transform.position;
            
                Vector3 randomPos = playerPos + new Vector3(randomPosList[Random.Range(0, randomPosList.Count)] * data.ratioDistanceCurve, 55, randomPosList[Random.Range(0, randomPosList.Count)] * data.ratioDistanceCurve);
                Vector3 fallPosition = caster.transform.position + randomPos;
            
                GameObject bottle = Pooler.Instance.Pop(Pooler.Key.Bottle);
                bottle.transform.position = fallPosition;
                bottle.GetComponent<BoxCollider>().size = new Vector3(data.impactSizeHarassment, 2, data.impactSizeHarassment);
            
                fallPosition.y = 0.5f;
                GameObject fx = VFXPooler.Instance.Pop(VFXPooler.Key.BottleVFX);
                fx.transform.position = fallPosition;
                fx.transform.localScale = new Vector3(data.impactSizeHarassment, 0.3f, data.impactSizeHarassment);
            
                bottle.GetComponent<BottleFalling>().Init(data.SpeedBottleHarassment, fx);
                yield return new WaitForSeconds(caster.data.delayBetweenBottleHarassment);
            }
        }

        public override void Execute()
        {
            caster.currentPattern = this;
            caster.StartCoroutine(ExecuteBottleHarassmentAnimation());
        }
    }
}