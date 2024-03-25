using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcam : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform followTarget;
    Vector3 offset;
    public Transform target; // ��ɫTransform����
    public float mouseSensitivity = 1.0f; // ���������
    //public Transform cameraPivot; // �⽫��֮ǰ�������м�����
    public float rotationSpeed = 100.0f; // �������ת�ٶ�
    public float distanceFromTarget = 3f; // �������Ŀ��ľ���
    public float xSpeed = 2f; // X����ת�ٶ�
    public float ySpeed = 2f; // Y����ת�ٶ�
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
        // �����Ҫ�����Χ�ƽ�ɫ��ת�������һ�˳��ӽ�)
        //transform.LookAt(target); // ������������ɫ
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
