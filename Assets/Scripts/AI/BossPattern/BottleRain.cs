using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "BottleRain", menuName = "ScriptableObjects/Pattern/BottleRain", order = 1)]
public class BottleRain : Pattern
{
    [SerializeField] private GameObject bottleAnimationPrefab;
    [SerializeField] private GameObject bottlePrefab;
    [SerializeField] private GameObject FXFallingPosPrefab;

    public override float GetDelay()
    {
        BossData data = caster.Data;
        float animationBottle = data.NbBottleRain * (data.delayBetweenBottleRain + 1.2f); // 1.2f = temps de l'animation up
        float animationFallBottle = data.NbBottleRain * (data.delayBetweenBottleRain + 1.2f); // 1.2f = temps de l'animation fall
        return animationBottle + data.delayBeforeFallingRain + animationFallBottle;
    }

    public override void TouchPlayer(PlayerController player)
    {
        player.Collision.Damage(caster.Data.damagePerBottleRain);
        Debug.Log("Touch Player by BottleRain");
    }

    public override void EndTrigger(GameObject obj)
    {
        obj.GetComponent<BottleFalling>().EndOfLife();
        Pooler.Instance.Depop(Key.Bottle, obj);
    }

    IEnumerator ExecuteBottleRainAnimation()
    {
        BossData data = caster.Data;
        for (int i = 0; i < data.NbBottleRain; i++)
        {
            Instantiate(bottleAnimationPrefab, caster.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(caster.Data.delayBetweenBottleRain);
        }
        yield return new WaitForSeconds(data.delayBeforeFallingRain);
        for (int i = 0; i < data.NbBottleRain; i++)
        {
            Vector3 randomPos = new Vector3(Utils.Utilities.RandomRangeWithExclusion(-data.MaxImpactRangeRain, data.MaxImpactRangeRain, -data.MinImpactRangeRain, data.MinImpactRangeRain),
                55, 
                Utils.Utilities.RandomRangeWithExclusion(-data.MaxImpactRangeRain, data.MaxImpactRangeRain, -data.MinImpactRangeRain, data.MinImpactRangeRain));
            
            Vector3 FallPosition = caster.transform.position + randomPos;

            GameObject bottle = Pooler.Instance.Pop(Key.Bottle);
            bottle.transform.position = FallPosition;
            bottle.GetComponent<BoxCollider>().size = new Vector3(data.ImpactSizeRain, 2, data.ImpactSizeRain);
            
            FallPosition.y = 0.5f;
            GameObject fx = Pooler.Instance.Pop(Key.FXBottle);
            fx.transform.position = FallPosition;
            fx.transform.localScale = new Vector3(data.ImpactSizeRain, 1, data.ImpactSizeRain);
            
            bottle.GetComponent<BottleFalling>().Init(data.SpeedBottleRain, fx);
            yield return new WaitForSeconds(caster.Data.delayBetweenBottleRain);
        }
    }
    
    public override void Execute()
    {
        caster.currentPattern = this;
        caster.StartCoroutine(ExecuteBottleRainAnimation());
    }
}
