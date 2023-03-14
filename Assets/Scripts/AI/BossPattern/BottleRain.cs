using System.Collections;
using Player;
using UnityEngine;
using Utils;

namespace AI.BossPattern
{
    [CreateAssetMenu(fileName = "BottleRain", menuName = "ScriptableObjects/Pattern/BottleRain", order = 1)]
    public class BottleRain : Pattern
    {
        [SerializeField] private GameObject bottleAnimationPrefab;

        public override float GetDelay()
        {
            BossData data = caster.data;
            float animationBottle = 4f; // 4f = temps de l'animation up
            float animationFallBottle = data.nbBottleRain * (data.delayBetweenBottleRain + 1.2f); // 1.2f = temps de l'animation fall
            return animationBottle + data.delayBeforeFallingRain + animationFallBottle;
        }

        public override void TouchPlayer(PlayerController player)
        {
            player.Collision.Damage(caster.data.damagePerBottleRain);
            Debug.Log("Touch Player by BottleRain");
        }

        public override void EndTrigger(GameObject obj)
        {
            obj.GetComponent<BottleFalling>().EndOfLife();
            Pooler.Instance.Depop(Key.Bottle, obj);
        }

        private IEnumerator ExecuteBottleRainAnimation()
        {
            BossData data = caster.data;
            caster.Animator.SetTrigger("ThrowBottle");
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(1f);
                Instantiate(bottleAnimationPrefab, caster.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(data.delayBeforeFallingRain);
            for (int i = 0; i < data.nbBottleRain; i++)
            {
                Vector3 randomPos = new Vector3(Utils.Utilities.RandomRangeWithExclusion(-data.maxImpactRangeRain, data.maxImpactRangeRain, -data.minImpactRangeRain, data.minImpactRangeRain),
                    55, 
                    Utils.Utilities.RandomRangeWithExclusion(-data.maxImpactRangeRain, data.maxImpactRangeRain, -data.minImpactRangeRain, data.minImpactRangeRain));
            
                Vector3 fallPosition = caster.transform.position + randomPos;

                GameObject bottle = Pooler.Instance.Pop(Key.Bottle);
                bottle.transform.position = fallPosition;
                bottle.GetComponent<BoxCollider>().size = new Vector3(data.impactSizeRain, 2, data.impactSizeRain);

                fallPosition.y = 0.5f;
                GameObject fx = Pooler.Instance.Pop(Key.FXBottle);
                fx.transform.position = fallPosition;
                fx.transform.localScale = new Vector3(data.impactSizeRain, 1, data.impactSizeRain);
            
                bottle.GetComponent<BottleFalling>().Init(data.SpeedBottleRain, fx);
                yield return new WaitForSeconds(caster.data.delayBetweenBottleRain);
            }
        }
    
        public override void Execute()
        {
            caster.currentPattern = this;
            caster.StartCoroutine(ExecuteBottleRainAnimation());
        }
    }
}
