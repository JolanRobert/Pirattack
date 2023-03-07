using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform[] focus;
    public Camera[] cameras;
    public Transform[] players;
    public float distance,angle;
    public bool separated;
    public Material splitScreenMat;
    public static CameraManager instance;
    public bool playersConnected;
    public Vector3 focusPos;
    //public UiIndicator uiIndicator;

    private void Awake()
    {
        instance = this;
    }

    public void InitializePlayer(Transform player)
    {
        if (players[0] == null)
        {
            players[0] = player;
        }
        else
        {
            players[1] = player;
            playersConnected = true;
        }
    }

    private void Update()
    {
        if(!playersConnected) return;
        if (!separated)
        {
            if (Vector3.SqrMagnitude(focus[0].position - focus[1].position) > distance * distance)
            {
                separated = true;
                cameras[1].gameObject.SetActive(true);
                splitScreenMat.SetInt("_Split",1);
                Separated();
            }
            else United();
        }
        else
        {
            if (Vector3.SqrMagnitude(focus[0].position - focus[1].position) < distance * distance)
            {
                separated = false;
                cameras[1].gameObject.SetActive(false);
                splitScreenMat.SetInt("_Split",0);
                United();
            }
            else Separated();
        }
    }

    void Separated()
    {
        focus[0].position = players[0].position + focusPos;
        focus[1].position = players[1].position + focusPos;
        //uiIndicator.UpdateIndicatorsSeparated();
        cameras[0].transform.position = focus[0].transform.position + (focus[1].transform.position - focus[0].transform.position).normalized * (distance / 2);
        cameras[1].transform.position = focus[1].transform.position + (focus[0].transform.position - focus[1].transform.position).normalized * (distance / 2);
        angle = Vector2.SignedAngle(Vector2.right,
            new Vector2(focus[0].transform.position.x, focus[0].transform.position.z) -
            new Vector2(focus[1].transform.position.x, focus[1].transform.position.z));
        float width = Mathf.Clamp((Vector3.Distance(focus[0].transform.position, focus[1].transform.position) - distance) * 0.05f,0,0.03f) ;
        splitScreenMat.SetFloat("_Angle",angle);
        splitScreenMat.SetFloat("_Width",width);
    }
    
    void United()
    {
        focus[0].position = players[0].position + focusPos;
        //uiIndicator.UpdateIndicatorsSingle();
        cameras[0].transform.position = (focus[0].transform.position + focus[1].transform.position) / 2;
    }
    
    
}
