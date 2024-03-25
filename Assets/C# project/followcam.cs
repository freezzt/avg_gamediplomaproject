using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcam : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform followTarget;
    Vector3 offset;
    public Transform target; // 角色Transform引用
    public float mouseSensitivity = 1.0f; // 鼠标灵敏度
    //public Transform cameraPivot; // 这将是之前创建的中间层对象
    public float rotationSpeed = 100.0f; // 摄像机旋转速度
    public float distanceFromTarget = 3f; // 摄像机离目标的距离
    public float xSpeed = 2f; // X轴旋转速度
    public float ySpeed = 2f; // Y轴旋转速度
    public float minY = -80f;
    public float maxY = 80f;
    private float x = 0f;
    private float y = 0f;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset + new Vector3(0, 2, 0);
        // 如果需要摄像机围绕角色旋转（比如第一人称视角)
        //transform.LookAt(target); // 让摄像机看向角色
        /*Vector3 v1 = target.position + (target.forward * -disfromtarget) + new Vector3(0, heightoffset, 0);
        transform.position = v1;*/
        mouselook();
        //cameraPivot.LookAt(transform);
        transform.LookAt(target);
    }
    void mouselook()
    {
        /*float mx = Input.GetAxis("Mouse X")*mouseSensitivity;
        float my = -Input.GetAxis("Mouse Y")*mouseSensitivity;
        Quaternion qx = Quaternion.Euler(0, mx, 0);
        Quaternion qy = Quaternion.Euler(my, 0, 0);
        cameraPivot.rotation = qx * cameraPivot.rotation;
        cameraPivot.rotation = cameraPivot.rotation * qy;
        float angle = cameraPivot.eulerAngles.x;
        if (angle > 180) { angle -= 360; }
        if (angle < -180) { angle += 360; }
        if(angle>80)
        {
            cameraPivot.eulerAngles = new Vector3(80, cameraPivot.eulerAngles.y, 0);
        }
        if(angle<-80)
        {
            cameraPivot.eulerAngles = new Vector3(-80, cameraPivot.eulerAngles.y, 0);
        }
        //Quaternion rotation = Quaternion.AngleAxis(mx * rotationspeed * Time.deltaTime, Vector3.up);
        //transform.rotation = rotation * Quaternion.LookRotation(target.up);*/
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime*rotationSpeed*mouseSensitivity;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime*rotationSpeed*mouseSensitivity;
        y = Mathf.Clamp(y, minY, maxY);
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distanceFromTarget;
        transform.rotation = rotation;
        transform.position = position;
    }
}
