using System.Collections;
using System.Collections.Generic;
using AI;
using AI.BossPattern;
using Player;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "BottleHarassment", menuName = "ScriptableObjects/Pattern/BottleHarassment", order = 1)]
public class BottleHarrasment : Pattern
{
    [SerializeField] private GameObject bottleAnimationPrefab;
    [SerializeField] private GameObject bottlePrefab;
    [SerializeField] private GameObject FXFallingPosPrefab;

    public override float GetDelay()
    {
        BossData data = caster.Data;
        float animationTime = ((55 / Time.deltaTime * 45f) * Time.deltaTime) / 1000f;
        float animationBottle = data.nbBottleHarassment * data.delayBetweenBottleHarassment + animationTime;
        float animationFallBottle = data.nbBottleHarassment * data.delayBetweenBottleHarassment + animationTime;
        return animationBottle + data.delayBeforeFallingHarassment + animationFallBottle;
    }

    public override void TouchPlayer(PlayerController player)
    {
        player.Collision.Damage(caster.Data.damagePerBottleHarassment);
        Debug.Log("Touch Player by BottleRain");
    }

    public override void EndTrigger(GameObject obj)
    {
        obj.GetComponent<BottleFalling>().EndOfLife();
        Pooler.Instance.Depop(Key.Bottle, obj);
    }

    IEnumerator ExecuteBottleHarassmentAnimation()
    {
        BossData data = caster.Data;
        for (int i = 0; i < data.nbBottleHarassment; i++)
        {
            Instantiate(bottleAnimationPrefab, caster.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(caster.Data.delayBetweenBottleHarassment);
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
            Vector3 playerPos = MyGameManager.Instance.Players[i % 2].transform.position;
            
            Vector3 randomPos = playerPos + new Vector3(randomPosList[Random.Range(0, randomPosList.Count)] * data.ratioDistanceCurve, 55, randomPosList[Random.Range(0, randomPosList.Count)] * data.ratioDistanceCurve);
            Vector3 FallPosition = caster.transform.position + randomPos;
            
            GameObject bottle = Pooler.Instance.Pop(Key.Bottle);
            bottle.transform.position = FallPosition;
            bottle.GetComponent<BoxCollider>().size = new Vector3(data.impactSizeHarassment, 2, data.impactSizeHarassment);
            
            FallPosition.y = 0.5f;
            GameObject fx = Pooler.Instance.Pop(Key.FXBottle);
            fx.transform.position = FallPosition;
            fx.transform.localScale = new Vector3(data.impactSizeHarassment, 1, data.impactSizeHarassment);
            
            bottle.GetComponent<BottleFalling>().Init(data.SpeedBottleHarassment, fx);
            yield return new WaitForSeconds(caster.Data.delayBetweenBottleHarassment);
        }
    }

    public override void Execute()
    {
        caster.currentPattern = this;
        caster.StartCoroutine(ExecuteBottleHarassmentAnimation());
    }
}