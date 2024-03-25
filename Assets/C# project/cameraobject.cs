using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraobject : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 1.0f; // 鼠标灵敏度
    public Transform cameraPivot; // 这将是之前创建的中间层对象
    //public Transform target; // 角色Transform引用
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //mouselook();
        //cameraPivot.LookAt(transform);
        if(Input.GetKey(KeyCode.LeftAlt)||Input.GetKey(KeyCode.RightAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Camera.main.transform.GetComponent<followcam>().enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Camera.main.transform.GetComponent<followcam>().enabled = true;
        }
    }
    void mouselook()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        Quaternion qx = Quaternion.Euler(0, mx, 0);
        Quaternion qy = Quaternion.Euler(my, 0, 0);
        transform.rotation = qx * transform.rotation;
        transform.rotation = transform.rotation * qy;
        float angle = transform.eulerAngles.x;
        if (angle > 180) { angle -= 360; }
        if (angle < -180) { angle += 360; }
        if (angle > 80)
        {
            transform.eulerAngles = new Vector3(80, transform.eulerAngles.y, 0);
        }
        if (angle < -80)
        {
            transform.eulerAngles = new Vector3(-80, transform.eulerAngles.y, 0);
        }
        //Quaternion rotation = Quaternion.AngleAxis(mx * rotationspeed * Time.deltaTime, Vector3.up);
        //transform.rotation = rotation * Quaternion.LookRotation(target.up);
    }
}
