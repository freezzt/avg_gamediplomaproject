using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float pmonster = 10.0f;//怪物警戒距离
    public float monsterspeed = 3f;//怪物走路速度
    Vector3 vmonster;//怪物游戏开始时初始位置
    public float mondis = 10.0f;//怪物活动范围
    public Animator anim;
    public float attackspeed;
    CharacterController cc;
    public GameObject wuq;
    void Start()
    {
        //player = GetComponent<Transform>();
        player = 游戏开始.player;
        List<GameObject> list = new List<GameObject>();
        foreach (Transform x in transform)
        {
            if (x.CompareTag("Barbarian 武器"))
            {
                list.Add(x.gameObject);
            }
            gettagchild(x, "Barbarian 武器", list);
        }
        wuq = list[0];
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        vmonster = transform.position;
        vmonster.y = (float)0.18;
        wuq.SetActive(false);
        attackspeed = anim.speed;
        //GameObject.Find(transform.name+"/Treasure_Chest").gameObject.SetActive(false);
    }
    public void gettagchild(Transform parent, string tagname, List<GameObject> list_y)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tagname))
            {
                list_y.Add(child.gameObject);
            }
            gettagchild(child, tagname, list_y);
        }
    }
    public int k = 1;
    public Vector3 vector3 = new Vector3(0, 0, 0);
    public bool stoptime = true;
    public float waittingtime = 0;
    public Vector3 playerv3 = new Vector3(0, 0, 0);
    public int y = 0, y1 = 0;
    public float distance;
    public float mdistance;
    // Update is called once per frame
    void Update()
    {
        if (player.activeSelf)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            mdistance = Vector3.Distance(vmonster, transform.position);
            //Debug.Log(distance + "玩家:" + player.transform.position + "  怪物:" + transform.position);
            //Debug.Log(mdistance + "原始位置:" + vmonster + "  怪物:" + transform.position);
            if (distance >= pmonster)
            {
                anim.speed = attackspeed;
                if (mdistance < mondis && stoptime)
                {
                    if (k == 1)
                    {
                        waittingtime += Time.deltaTime;
                        anim.SetFloat("speed", 0);
                        if (waittingtime > 3.0f)
                        {
                            float random1 = UnityEngine.Random.Range(-1.0f, 1.0f);
                            float random2 = UnityEngine.Random.Range(-1.0f, 1.0f);
                            vector3.x = random1; vector3.z = random2;
                            anim.SetFloat("speed", 0);
                            transform.rotation = Quaternion.LookRotation(vector3);
                            waittingtime = 2.0f;
                            y1 = 1;
                        }
                        if (waittingtime > 0.1f && y1 == 1)
                        {
                            waittingtime -= Time.deltaTime * 2;
                        }
                        else if (y1 == 1)
                        {
                            k--; y1 = 0;
                            waittingtime = 0.0f;
                        }

                    }
                    if (mdistance < mondis && k == 0)
                    {
                        anim.SetFloat("speed", monsterspeed);
                        cc.Move(vector3 * Time.deltaTime * monsterspeed * 3);
                    }
                    mdistance = Vector3.Distance(vmonster, transform.position);
                    if (mdistance >= mondis)
                    {
                        stoptime = false;
                        anim.SetFloat("speed", 0);
                    }
                }
                else if (mdistance >= mondis || !stoptime)
                {
                    waittingtime += Time.deltaTime;

                    if (waittingtime > 3.0f)
                    {
                        waittingtime = 2.0f;
                        y = 1;
                    }
                    if ((waittingtime > 0.1f && y == 1))
                    {
                        waittingtime -= Time.deltaTime * 2;
                        anim.SetFloat("speed", 0);
                        //wuq.SetActive(false);
                    }
                    else if (y == 1)
                    {
                        //y = 0; 
                        waittingtime = 0.0f;
                        vector3 = vmonster - transform.position;
                        vector3 = vector3.normalized;
                        anim.SetFloat("speed", monsterspeed);
                        transform.rotation = Quaternion.LookRotation(vector3);
                        cc.Move(vector3 * Time.deltaTime * monsterspeed);
                    }
                    if ((int)mdistance * 10 == 0)
                    {
                        k = 1;
                        stoptime = true;
                        waittingtime = 0;
                        y1 = 0; y = 0;
                        vector3 = new Vector3(0, 0, 0);
                    }
                }
            }
            else
            {
                k = 1;
                stoptime = false;
                waittingtime = 0;
                y1 = 0; y = 0;
                //anim.speed = attackspeed / 2;
                vector3 = new Vector3(0, 0, 0);
                playerv3 = player.transform.position - transform.position;
                playerv3.y = 0;
                playerv3 = playerv3.normalized;
                wuq.SetActive(true);
                anim.SetFloat("speed", monsterspeed * 2);
                anim.SetBool("run", true);
                transform.rotation = Quaternion.LookRotation(playerv3);
                cc.Move(playerv3 * Time.deltaTime * monsterspeed);
            }
        }
    }
    IEnumerator waittime(float x)
    {
        yield return new WaitForSeconds(x);
    }
    public void runbegin()
    {
        anim.SetBool("run", false);
        //anim.speed = attackspeed / 2;
    }
    public void runend()
    {
        //wuq.SetActive(false);
    }
    public void idlebegin()
    {
        //anim.SetBool("run", false);
        //anim.speed = attackspeed;
        wuq.SetActive(false);
    }
}