using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using static UnityEngine.GameObject;
using UnityEngine.UI;
public class people1 : MonoBehaviour
{
    peoplecharacter player;
    LineRenderer throwLine;
    //public Camera camera;
    [HideInInspector]
    public Vector3 curGroundPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<peoplecharacter>();
        throwLine = GetComponentInChildren<LineRenderer>();
        //camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input;
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(input.magnitude>1.0f)
        {
            input = input.normalized;
        }
        player.curinput = input;
        //bool fire = false;
        //bool touch = touchgroundpos();
        player.UpdateMove();
        //player.UpdateAction(curGroundPoint, fire);
        //player.UpdateAnim(curGroundPoint);
    }
    public bool touchgroundpos()
    {
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(!Physics.Raycast(ray,out hitInfo,1000,LayerMask.GetMask("Ground")))
        {
            return false;
        }
        curGroundPoint = hitInfo.point;
        return true;
    }
}
