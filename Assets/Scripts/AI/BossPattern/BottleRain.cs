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
            float animationBottle = 5.5f; // 3f = temps de l'animation up
            float animationFallBottle = data.nbBottleHarassment * data.delayBetweenBottleHarassment + 1.2f;
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
            Pooler.Instance.Depop(Pooler.Key.Bottle, obj);
        }

        private IEnumerator ExecuteBottleRainAnimation()
        {
            BossData data = caster.data;
            caster.Animator.SetTrigger("ThrowBottle");
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 4; i++)
            {
                Instantiate(bottleAnimationPrefab, caster.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.75f);
            }

            yield return new WaitForSeconds(data.delayBeforeFallingRain);

            for (int i = 0; i < data.nbBottleRain; i++)
            {
                Vector3 randomPos = new Vector3(
                    Utilities.RandomRangeWithExclusion(-data.maxImpactRangeRain, data.maxImpactRangeRain,
                        -data.minImpactRangeRain, data.minImpactRangeRain),
                    55,
                    Utilities.RandomRangeWithExclusion(-data.maxImpactRangeRain, data.maxImpactRangeRain,
                        -data.minImpactRangeRain, data.minImpactRangeRain));

                Vector3 fallPosition = caster.transform.position + randomPos;

                GameObject bottle = Pooler.Instance.Pop(Pooler.Key.Bottle);
                bottle.SetActive(false);
                bottle.transform.position = fallPosition;
                bottle.GetComponent<BoxCollider>().size = new Vector3(data.impactSizeRain, 2, data.impactSizeRain);
                bottle.SetActive(true);

                fallPosition.y = 0.5f;
                GameObject fx = VFXPooler.Instance.Pop(VFXPooler.Key.BottleVFX);
                fx.transform.position = fallPosition;
                fx.transform.localScale = new Vector3(data.impactSizeRain, 0.3f, data.impactSizeRain);

                bottle.GetComponent<BottleFalling>().Init(data.speedBottleRain, fx);
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