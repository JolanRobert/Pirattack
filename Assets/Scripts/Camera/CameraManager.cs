using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public Transform[] focus;
    public Transform[] players;
    public Camera[] cameras;
    [SerializeField] private float distance;
    private float distanceRectified;
    public float angle;
    [SerializeField] private Material splitScreenMat;
    public static CameraManager instance;
    [SerializeField] private bool playersConnected,separated;
    [SerializeField] private Vector3 focusPos;
    [SerializeField] private UiIndicator uiIndicator;

    private void Awake()
    {
        instance = this;
    }

    public void InitializePlayer(PlayerInput player)
    {
        if (players[0] == null) players[0] = player.transform;
        else
        {
            players[1] = player.transform;
            playersConnected = true;
        }
    }

    private void Update()
    {
        distanceRectified = distance * Mathf.Lerp(1,1.78f,(focus[0].position - focus[1].position).normalized.x);
        if(!playersConnected) return;
        if (!separated)
        {
            if (Vector3.SqrMagnitude(focus[0].position - focus[1].position) > distanceRectified * distanceRectified)
            {
                separated = true;
                cameras[1].gameObject.SetActive(true);
                cameras[2].gameObject.SetActive(true);
                Separated();
            }
            else United();
        }
        else
        {
            if (Vector3.SqrMagnitude(focus[0].position - focus[1].position) < distanceRectified * distanceRectified)
            {
                separated = false;
                cameras[1].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(false);
                United();
            }
            else Separated();
        }
    }

    void Separated()
    {
        focus[0].position = players[0].position + focusPos;
        focus[1].position = players[1].position + focusPos;
        uiIndicator.UpdateIndicators(true);
        cameras[0].transform.position = focus[0].transform.position + (focus[1].transform.position - focus[0].transform.position).normalized * (distanceRectified / 2);
        cameras[1].transform.position = focus[1].transform.position + (focus[0].transform.position - focus[1].transform.position).normalized * (distanceRectified / 2);
        angle = Vector2.SignedAngle(Vector2.right,
            new Vector2(focus[0].transform.position.x, focus[0].transform.position.z) -
            new Vector2(focus[1].transform.position.x, focus[1].transform.position.z));
        float width = Mathf.Clamp((Vector3.Distance(focus[0].transform.position, focus[1].transform.position) - distanceRectified) * 0.05f,0,0.03f) ;
        splitScreenMat.SetFloat("_Angle",angle);
        splitScreenMat.SetFloat("_Width",width);
    }
    
    void United()
    {
        focus[0].position = players[0].position + focusPos;
        focus[1].position = players[1].position + focusPos;
        uiIndicator.UpdateIndicators(false);
        cameras[0].transform.position = (focus[0].transform.position + focus[1].transform.position) / 2;
    }
}
