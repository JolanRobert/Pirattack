using System;
using System.Collections.Generic;
using MyBox;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class UiIndicator : MonoBehaviour
{
    public static UiIndicator instance;
    
    [SerializeField] private CameraManager cameraManager;
    [SerializeField, ReadOnly] private List<IndicObj> obj;
    [SerializeField, ReadOnly] private List<Transform> indics;
    [SerializeField] private RectTransform boxRect;
    [SerializeField] private GameObject prefabNoColor;
    [SerializeField] private GameObject prefabRed;
    [SerializeField] private GameObject prefabBlue;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateIndicators(bool separated)
    {
        for (int i = 0; i < obj.Count; i++)
        {
            if (separated)
            {
                UpdateIndicatorSeparated(i);
            }
            else
            {
                UpdateIndicatorSingle(i);
            }
        }
    }

    public Image[] AddObject(GameObject newObj, PlayerColor color)
    {
        IndicObj added = new IndicObj();
        added.obj = newObj;
        added.color = color;
        obj.Add(added);

        Image[] res = new Image[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newIndic = Instantiate(color == PlayerColor.None ? prefabNoColor : color == PlayerColor.Blue ? prefabBlue : prefabRed, boxRect.position, Quaternion.identity, boxRect);
            newIndic.SetActive(false);
            res[i] = newIndic.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            indics.Add(newIndic.transform);
        }

        return res;
    }
    
    public void RemoveObject(GameObject objToRemove)
    {
        int index = 0;
        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].obj == objToRemove)
            {
                index = i;
            }
        }
        
        Destroy(indics[index*2+1].gameObject);
        Destroy(indics[index*2].gameObject);
        
        indics.RemoveAt(index * 2+1);
        indics.RemoveAt(index * 2);
        obj.RemoveAt(index);
    }
    

    public void UpdateIndicatorSingle(int objIndex)
    {
        if (CheckVisibilitySingle(objIndex))
        {
            indics[objIndex*2].gameObject.SetActive(true);
        }
        else
        {
            indics[objIndex*2].gameObject.SetActive(false);
        }
        indics[objIndex*2+1].gameObject.SetActive(false);
        Vector3 focus = (cameraManager.players[0].position + cameraManager.players[1].position) / 2;
        Vector3 dir = obj[objIndex].obj.transform.position - focus;
        Vector2 pos = cameraManager.cameras[0].WorldToScreenPoint(focus + dir.normalized);
        indics[objIndex*2].position = FindPointOnRectBorder(pos - (boxRect.rect.center + (Vector2)boxRect.position),
            boxRect.rect.center + (Vector2)boxRect.position,boxRect);
        indics[objIndex*2].GetChild(0).rotation = Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up,(Vector2)indics[objIndex*2].position - (boxRect.rect.center+ (Vector2)boxRect.position)));
        
        Debug.DrawLine(boxRect.rect.center + (Vector2)boxRect.position,pos,Color.red);
    }
    
    public void UpdateIndicatorSeparated(int objIndex)
    {
        for (int i = 0; i < 2; i++)
        {
            if (CheckVisibility(i,objIndex*2+i,objIndex))
            {
                indics[objIndex*2+i].gameObject.SetActive(true);
            }
            else indics[objIndex*2+i].gameObject.SetActive(false);
            UpdatePositionForCamera(i,objIndex*2+i,objIndex);
        }
    }

    public void UpdatePositionForCamera(int player,int index,int objIndex)
    {
        Vector3 focus = cameraManager.players[player].position;
        Vector3 dir = obj[objIndex].obj.transform.position - focus;
        Vector2 pos = cameraManager.cameras[player].WorldToScreenPoint(focus + dir.normalized);
        Vector2 center = cameraManager.cameras[player].WorldToScreenPoint(focus);
        Vector2 splitDir = Quaternion.Euler(0,0,cameraManager.angle) * Vector2.up;
        Vector2 splitNormal = Quaternion.Euler(0, 0, player == 0 ? -90 : 90) * splitDir;
        Vector2 splitCenter = boxRect.rect.center+(Vector2) boxRect.position + splitNormal * 70;
        Vector2 splitPos = Intersection(center, center + (pos - center).normalized * 2000,
            splitCenter + splitDir * 1500, splitCenter - splitDir * 1500, out bool found);
        if (found && Vector2.Angle(splitNormal,(pos - center).normalized) > 90 && boxRect.rect.Contains(splitPos - (Vector2)boxRect.position))
        {
            indics[index].position = splitPos;
        }
        else
        {
            indics[index].position = FindPointOnRectBorder(pos - center,
                center,boxRect);
        }
        indics[index].GetChild(0).rotation = Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up,(Vector2)indics[index].position - center));
    }

    public bool CheckVisibility(int player,int index,int objIndex)
    {
        if (PlayerManager.Players[player].Color.PColor != obj[objIndex].color)
        {
            return false;
        }
        Vector2 pos = cameraManager.cameras[player].WorldToScreenPoint(obj[objIndex].obj.transform.position);
        Vector2 splitNormal = Quaternion.Euler(0, 0, player == 0 ? cameraManager.angle-90 : cameraManager.angle+90) * Vector2.up;
        if (boxRect.rect.Contains(pos - (Vector2) boxRect.position))
        {
            if (Vector2.Angle(splitNormal, pos - (boxRect.rect.center + (Vector2) boxRect.position)) < 90)
            {
                return false;
            }
        }
        return true;
    }
    public bool CheckVisibilitySingle(int objIndex)
    {
        Vector2 pos = cameraManager.cameras[0].WorldToScreenPoint(obj[objIndex].obj.transform.position);
        if (boxRect.rect.Contains(pos - (Vector2) boxRect.position))
        {
            return false;
        }
        return true;
    }
    
    public Vector2 FindPointOnRectBorder(Vector2 dir,Vector2 center,RectTransform rect)
    {
        Vector2 pos = rect.rect.position + (Vector2)rect.position;
        Rect newRect = new Rect(pos, rect.rect.size);
        float angleSup = Vector2.SignedAngle(Vector2.up, newRect.max - center);
        float angleInf = Vector2.SignedAngle(Vector2.up, new Vector2(newRect.xMax,newRect.yMin) - center);
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        
        if (angle > angleSup && angle < -angleSup)
        {
            Vector2 intersection = Intersection(center, center + dir.normalized * 1000, new Vector2(newRect.xMin, newRect.yMax), new Vector2(newRect.xMax, newRect.yMax), out bool found);
            return intersection;
        }
        if (angle < angleInf || angle > -angleInf)
        {
            Vector2 intersection = Intersection(center, center + dir.normalized * 1000, new Vector2(newRect.xMin, newRect.yMin), new Vector2(newRect.xMax, newRect.yMin), out bool found);
            return intersection;
        }
        if (angle > 0)
        {
            Vector2 intersection = Intersection(center, center + dir.normalized * 1000, new Vector2(newRect.xMin, newRect.yMin), new Vector2(newRect.xMin, newRect.yMax), out bool found);
            return intersection;
        } 
        if (angle < 0)
        {
            Vector2 intersection = Intersection(center, center + dir.normalized * 1000, new Vector2(newRect.xMax, newRect.yMin), new Vector2(newRect.xMax, newRect.yMax), out bool found);
            return intersection;
        }
        return Vector2.zero;
    }
    
    public Vector2 Intersection(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2,out bool found)
    {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);
        if (tmp == 0)
        {
            found = false;
            return Vector2.zero;
        }
        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;
        found = true;
        return new Vector2(
            B1.x + (B2.x - B1.x) * mu,
            B1.y + (B2.y - B1.y) * mu
        );
    }

    [Serializable]
    public class IndicObj
    {
        public PlayerColor color;
        public GameObject obj;
    }
}
