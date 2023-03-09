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
        float animationBottle = data.NbBottle * (data.delayBetweenBottle + 1.2f); // 1.2f = temps de l'animation up
        float animationFallBottle = data.NbBottle * (data.delayBetweenBottle + 1.2f); // 1.2f = temps de l'animation fall
        return animationBottle + data.delayBeforeFalling + animationFallBottle;
    }

    public override void TouchPlayer(PlayerController player)
    {
        player.Collision.Damage(caster.Data.damagePerBottle);
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
        for (int i = 0; i < data.NbBottle; i++)
        {
            Instantiate(bottleAnimationPrefab, caster.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(caster.Data.delayBetweenBottle);
        }
        yield return new WaitForSeconds(data.delayBeforeFalling);
        for (int i = 0; i < data.NbBottle; i++)
        {
            Vector3 randomPos = new Vector3(Utils.Utilities.RandomRangeWithExclusion(-data.MaxImpactRange, data.MaxImpactRange, -data.MinImpactRange, data.MinImpactRange),
                55, 
                Utils.Utilities.RandomRangeWithExclusion(-data.MaxImpactRange, data.MaxImpactRange, -data.MinImpactRange, data.MinImpactRange));
            Vector3 FallPosition = caster.transform.position + randomPos;

            GameObject bottle = Pooler.Instance.Pop(Key.Bottle);
            bottle.transform.position = FallPosition;
            bottle.GetComponent<BoxCollider>().size = new Vector3(data.ImpactSize, 2, data.ImpactSize);
            
            FallPosition.y = 0.5f;
            GameObject fx = Pooler.Instance.Pop(Key.FXBottle);
            fx.transform.position = FallPosition;
            fx.transform.localScale = new Vector3(data.ImpactSize, 1, data.ImpactSize);
            
            bottle.GetComponent<BottleFalling>().Init(data.SpeedBottle, fx);
            yield return new WaitForSeconds(caster.Data.delayBetweenBottle);
        }
    }
    
    public override void Execute()
    {
        caster.currentPattern = this;
        caster.StartCoroutine(ExecuteBottleRainAnimation());
    }
}
