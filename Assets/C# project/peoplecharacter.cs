using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peoplecharacter : MonoBehaviour
{
    public float movespeed = 4.0f;
    public float hp = 100;
    public float walkrun = 0;
    [HideInInspector]
    public bool dead = false;
    [HideInInspector]
    public Vector3 curinput;
    CharacterController cc;
    CapsuleCollider cap;
    people1 playerinput;
    public Transform cameratransfrom;
    Animator anim;
    public float attackspeed;
    public string curitem;
    public float rotationSpeed = 100.0f; // 角色旋转速度
    public float movementSpeed = 5f;
    public float tiaowaittime = 1f;    //跳跃等待时间
    public float jumpForce = 10.0f; // 跳跃力
    public float gravity = -9.81f; // 重力加速度
    private Vector3 moveDirection;
    Vector3 bzhong = new Vector3();
    GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerinput = GetComponent<people1>();
        anim = GetComponent<Animator>();
        curitem = "none";
        weapon = GameObject.FindGameObjectWithTag("武器");
        weapon.SetActive(false);
        游戏开始.canvas界面.transform.Find("Button F收集").gameObject.SetActive(false);
        游戏开始.canvas界面.transform.Find("Image 奖励描述").gameObject.SetActive(false);
        attackspeed = anim.speed;
        /*Bounds bounds = GetComponent<MeshFilter>().sharedMesh.bounds;
        Debug.Log(bounds.size);
        Debug.Log(bounds.center.y);*/
    }

    // Update is called once per frame
    public void UpdateMove()
    {
        if (dead) return;
        float curspeed = movespeed;
        switch (curitem)
        {
            case "none":
                break;
            case "handgun":            //手枪
                {
                    curspeed = movespeed / 2.0f;
                }
                break;
            case "cigar":         //诱饵
                {
                    curspeed = movespeed / 2.0f;
                }
                break;
            case "trap":         //陷阱
                {
                    curspeed = 0;
                }
                break;
            case "knife":           //匕首
                curspeed = movespeed / 2.0f;
                break;
        }
        //重力下落
        Vector3 v = curinput;
        if (!cc.isGrounded)
        {
            v.y = -0.5f;
        }
        //cc.Move(v * curspeed * Time.deltaTime);
        // 角色转向也基于摄像机视角
        //Debug.Log(v.y);
        if(Input.GetKey(KeyCode.U))
        {
            movespeed = 16.0f;
        }
        if (Input.GetKey(KeyCode.N))
        {
            movespeed = 4.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            turnto(cameratransfrom.transform.forward);
            //anim.SetTrigger("Walk");
        }
        if (Input.GetKey(KeyCode.S))
        {
            turnto(-cameratransfrom.transform.forward);
            //anim.SetTrigger("Walk");
        }
        if (Input.GetKey(KeyCode.A))
        {
            turnto(-cameratransfrom.transform.right);
            //anim.SetTrigger("Walk");
        }
        if (Input.GetKey(KeyCode.D))
        {
            turnto(cameratransfrom.transform.right);
            //anim.SetTrigger("Walk");
        }
        if (Input.GetKey(KeyCode.K))
            anim.SetBool("roll", true);
        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetActive(true);
            anim.SetBool("attack", true);
            anim.speed = attackspeed * 2;
        }
        if (Input.GetMouseButton(1))
        {
            walkrun = 1;
            curspeed *= 2;
        }
        else walkrun = 0;
        Vector3 moveuser = Camera.main.transform.forward * v.z + Camera.main.transform.right * v.x;
        cc.Move(moveuser * curspeed * Time.deltaTime);
        if (moveuser != Vector3.zero)
        {
            anim.SetBool("walk", true);
            //anim.speed = attackspeed;
        }
        else anim.SetBool("walk", false);
        anim.SetFloat("speed", curspeed);
        //cc.Move(bzhong * Time.deltaTime );
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * cc.height * 0.5f, -Vector3.up, out hit, cc.height))
        {
            //Debug.Log(Mathf.Abs(hit.distance - cc.height * 0.5f));
        }
        float hitabs = Mathf.Abs(hit.distance - cc.height * 0.5f);
        if (hitabs<=0.1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //cc.Move(-bzhong*Time.deltaTime*50);
                //StartCoroutine(waittime(tiaowaittime));
                bzhong.y = jumpForce;
                Debug.Log("接触地面");
                anim.SetBool("jump", true);
                //
            }
            else bzhong.y = 0;
        }
        else
            bzhong.y += gravity * Time.deltaTime;
        
        cc.Move(bzhong * Time.deltaTime);

        }
        public void UpdateAnim(Vector3 curinputpos)
        {
            if (dead)
            {
                anim.SetFloat("speed", 0);
                return;
            }
            //anim.SetFloat("speed", cc.velocity.magnitude / movespeed);
            /*if(curinput.magnitude>0.01f)
            {
                //transform.rotation = Quaternion.LookRotation(curinput);
            }*/
        }
        void turnto(Vector3 v)
        {
            Quaternion q = Quaternion.identity;
            q.SetLookRotation(v);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, q.eulerAngles.y, 0), Time.deltaTime * 40);
        }
        IEnumerator waittime(float x)
        {
            yield return new WaitForSeconds(x);
        }
    void jumpstop()
    {
        anim.SetBool("jump", false);
        //Debug.Log("起跳结束");
    }
    void jumpend()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * cc.height * 0.5f, -Vector3.up, out hit, cc.height))
            Debug.Log(Mathf.Abs(hit.distance - cc.height * 0.5f));
        float hitabs = Mathf.Abs(hit.distance - cc.height * 0.5f);
        if (hitabs > 0.01)
        {
            bzhong.y += -hitabs*8;//gravity * Time.deltaTime;
        }
        cc.Move(bzhong * Time.deltaTime);
    }
    void rollstop()
    {
        anim.SetBool("roll", false);
    }
    void attackbegin()
    {
        anim.speed = attackspeed * 2;
    }
    public void attackend()
    {
        anim.SetBool("attack", false);
        //weapon.SetActive(false);
        anim.speed = attackspeed;
    }
    public void idlebegin()
    {
        anim.speed = attackspeed;
        weapon.SetActive(false);
    }
    public void walkbegin()
    {
        anim.speed = attackspeed;
    }
    }