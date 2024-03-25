using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 按键提示 : MonoBehaviour
{
    /*public Transform target1;// 需要跟随的目标物体
    public RectTransform rectTransform;// UI血条组件的RectTransform
    [SerializeField]
    private Vector3 offset = new Vector3(-0.5f, 1.0f, -1.0f);// UI相对于目标物的偏移量
    */
    public Transform tf;
    public bool y = true;
    void Start()
    {

    }
    public void Update()
    {
        //rectTransform.position = target1.position + offset;
        if(Input.GetKey(KeyCode.F))
        {
            if (y)
            {
                tf.GetComponent<宝箱>().shouji();
                y = false;
            }
        }
    }
    /*void LateUpdate()
    {
        Vector3 forwarddir = Camera.main.transform.forward;
        rectTransform.rotation = Quaternion.LookRotation(forwarddir);
    }
    public bool isobjectvisibleincamera(RectTransform rectTransform, Camera camera)
    {
        Vector3 screenpoint = camera.WorldToViewportPoint(rectTransform.position);
        return screenpoint.x >= 0 && screenpoint.x <= 1 && screenpoint.y >= 0 && screenpoint.y <= 1;
    }
    /*public void shouji()
    {
        Quaternion quaternion1 = Quaternion.Euler(new Vector3(45, 0, 0));
        Quaternion quaternion = Quaternion.Slerp(transform.Find("Treasure_Chest_Up").transform.rotation, quaternion1, Time.deltaTime * rowspeed);
        transform.Find("Treasure_Chest_Up").transform.rotation = quaternion;
    }*/
}
