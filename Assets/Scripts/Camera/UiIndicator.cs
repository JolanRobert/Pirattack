using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using MyBox;
using Player;
using UnityEngine;

public class UiIndicator : MonoBehaviour
{
    public static UiIndicator instance;
    
    [SerializeField] private CameraManager cameraManager;
    [SerializeField, ReadOnly] private List<IndicObj> obj;
    [SerializeField, ReadOnly] private List<UINotif> indics;
    [SerializeField] private RectTransform boxRect;
    [SerializeField] private GameObject prefabNoColor;
    [SerializeField] private GameObject prefabRed;
    [SerializeField] private GameObject prefabBlue;
    [SerializeField] private float notifFadeTime;

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

    public UINotif[] AddObject(GameObject newObj, PlayerColor color, float yOffset)
    {
        IndicObj added = new IndicObj
        {
            obj = newObj,
            color = color,
            yOffset = yOffset
        };
        obj.Add(added);

        UINotif[] res = new UINotif[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newIndic = Instantiate(color == PlayerColor.None ? prefabNoColor : color == PlayerColor.Blue ? prefabBlue : prefabRed, boxRect.position, Quaternion.identity, boxRect);
            res[i] = newIndic.GetComponent<UINotif>();
            res[i].canvasGroup.alpha = 0;
            indics.Add(res[i]);
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
        
        indics[index * 2 + 1].canvasGroup.DOKill();
        indics[index * 2].canvasGroup.DOKill();
        
        Destroy(indics[index*2+1].gameObject);
        Destroy(indics[index*2].gameObject);
        
        indics.RemoveAt(index * 2+1);
        indics.RemoveAt(index * 2);
        obj.RemoveAt(index);
    }

    public void UpdateIndicatorSeparated(int objIndex)
    {
        for (int i = 0; i < 2; i++)
        {
            if (PlayerManager.Players[i].Color.PColor != obj[objIndex].color)
            {
                indics[objIndex * 2 + i].DoAlpha(0, notifFadeTime);
            }
            else
            {
                indics[objIndex * 2 + i].DoAlpha(1, notifFadeTime);
            }
            
            if (CheckVisibility(i,objIndex*2+i,objIndex))
            {
                UpdatePositionForCamera(i,objIndex*2+i,objIndex);
            }
            else UpdatePositionOnVisible(i,objIndex*2+i,objIndex);
        }
    }
    
    public void UpdateIndicatorSingle(int objIndex)
    {
        indics[objIndex * 2 + 1].DoAlpha(0, notifFadeTime);
        indics[objIndex * 2].DoAlpha(1, notifFadeTime);
        if (CheckVisibilitySingle(objIndex))
        {
            UpdatePositionForSingle(objIndex*2,objIndex);
        }
        else UpdatePositionOnVisible(0,objIndex*2,objIndex);
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
            indics[index].transform.position = splitPos;
        }
        else
        {
            indics[index].transform.position = FindPointOnRectBorder(pos - center, center,boxRect);
            Debug.DrawRay(center,(pos - center)*1000,Color.red);
        }
        indics[index].pinHead.rotation = Quaternion.Lerp(indics[index].pinHead.rotation,Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up,(Vector2)indics[index].transform.position - center)),5*Time.deltaTime);
    }
    
    public void UpdatePositionForSingle(int index,int objIndex)
    {
        Vector3 focus = (cameraManager.players[0].position + cameraManager.players[1].position) / 2;
        Vector3 dir = obj[objIndex].obj.transform.position - focus;
        Vector2 pos = cameraManager.cameras[0].WorldToScreenPoint(focus + dir.normalized);
        Vector2 center = cameraManager.cameras[0].WorldToScreenPoint(focus);
        
        indics[index].transform.position = FindPointOnRectBorder(pos - center, center,boxRect);
        indics[index].pinHead.rotation = Quaternion.Lerp(indics[index].pinHead.rotation,Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up,(Vector2)indics[index].transform.position - center)),5*Time.deltaTime);
    }
    
    public void UpdatePositionOnVisible(int player,int index,int objIndex)
    {
        Vector2 pos = cameraManager.cameras[player].WorldToScreenPoint(obj[objIndex].obj.transform.position);
        indics[index].transform.position = pos + Vector2.up * obj[objIndex].yOffset;
        indics[index].pinHead.rotation = Quaternion.Lerp(indics[index].pinHead.rotation,Quaternion.Euler(0,0,180),5*Time.deltaTime);
    }

    public bool CheckVisibility(int player,int index,int objIndex)
    {
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
        float angleSupNeg = Vector2.SignedAngle(Vector2.up, new Vector2(newRect.xMin,newRect.yMax) - center);
        float angleInfNeg = Vector2.SignedAngle(Vector2.up, newRect.min - center);
        float angle = Vector2.SignedAngle(Vector2.up, dir);
        
        Debug.DrawRay(center,Quaternion.Euler(0,0,angleSup) * Vector3.up * 2000,Color.green);
        Debug.DrawRay(center,Quaternion.Euler(0,0,angleInf) * Vector3.up * 2000,Color.green);
        Debug.DrawRay(center,Quaternion.Euler(0,0,angleSupNeg) * Vector3.up * 2000,Color.green);
        Debug.DrawRay(center,Quaternion.Euler(0,0,angleInfNeg) * Vector3.up * 2000,Color.green);
        
        
        if (angle > angleSup && angle < angleSupNeg)
        {
            Vector2 intersection = Intersection(center, center + dir.normalized * 1000, new Vector2(newRect.xMin, newRect.yMax), new Vector2(newRect.xMax, newRect.yMax), out bool found);
            return intersection;
        }
        if (angle < angleInf || angle > angleInfNeg)
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
        public float yOffset;
    }
}
