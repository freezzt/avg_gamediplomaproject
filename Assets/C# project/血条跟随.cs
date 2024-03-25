using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 血条跟随 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target1;// 需要跟随的目标物体
    public RectTransform rectTransform;// UI血条组件的RectTransform
    [SerializeField]
    private Vector3 offset = new Vector3(0, 2.5f, 0);// UI相对于目标物的偏移量
    void Start()
    {
        
    }
    public void Update()
    {
        //rectTransform.position = Camera.main.WorldToScreenPoint(target1.position + offset);
        rectTransform.position = target1.position + offset;
        //rectTransform.position = Camera.main.WorldToScreenPoint(rectTransform.position);
        //bool isonscreen = isobjectvisibleincamera(rectTransform, Camera.main);
        //rectTransform.gameObject.SetActive(isonscreen);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 forwarddir = Camera.main.transform.forward;
        rectTransform.rotation = Quaternion.LookRotation(forwarddir);
    }
    public bool isobjectvisibleincamera(RectTransform rectTransform,Camera camera)
    {
        Vector3 screenpoint = camera.WorldToViewportPoint(rectTransform.position);
        return screenpoint.x >= 0 && screenpoint.x <= 1 && screenpoint.y >= 0 && screenpoint.y <= 1;
    }
}
