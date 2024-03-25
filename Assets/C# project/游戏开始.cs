using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class 游戏开始 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject canvas开始;
    public static GameObject canvas信息;
    public static GameObject canvas界面;
    public static GameObject canvas背包;
    public static GameObject canvas商城;
    public static GameObject canvas设置;
    public static GameObject canvas任务;
    public static GameObject canvas好友;
    public static GameObject player;
    public static GameObject camera1;
    //public static GameObject[] monster;
    public static GameObject lifebar;
    public static GameObject canvas其他;
    public void Awake()
    {
        canvas开始 = GameObject.FindGameObjectWithTag("UI1");
        canvas信息 = GameObject.FindGameObjectWithTag("UI-信息");
        canvas界面 = GameObject.FindGameObjectWithTag("UI 游戏界面");
        canvas背包 = GameObject.FindGameObjectWithTag("UI 背包");
        canvas商城 = GameObject.FindGameObjectWithTag("UI 商城");
        canvas设置 = GameObject.FindGameObjectWithTag("UI 设置");
        canvas任务 = GameObject.FindGameObjectWithTag("UI 任务");
        canvas好友 = GameObject.FindGameObjectWithTag("UI 好友");
        canvas其他 = GameObject.FindGameObjectWithTag("其他");
        lifebar = GameObject.FindGameObjectWithTag("怪物血条");
        player = GameObject.FindGameObjectWithTag("Player");
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");
        //monster = GameObject.FindGameObjectsWithTag("怪物");
        lifebar.SetActive(false);
        canvas其他.SetActive(false);
        camera1.SetActive(false);
        player.SetActive(false);
        //for(int i=0;i<monster.Length;i++)
        //monster[i].SetActive(false);
        canvas开始.SetActive(true);
        canvas信息.SetActive(false);
        canvas界面.SetActive(false);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
    }
    public void Start()
    {
        
    }
    public void gamestartclick()
    {
        /*GameObject canvas开始;
        GameObject canvas信息;
        GameObject canvas界面;
        GameObject canvas背包;
        GameObject canvas商城;
        GameObject canvas设置;
        GameObject canvas任务;
        GameObject canvas好友;
        GameObject player;
        GameObject camera1;
        canvas开始 = GameObject.FindGameObjectWithTag("UI1");
        canvas信息 = GameObject.FindGameObjectWithTag("UI-信息");
        canvas界面 = GameObject.FindGameObjectWithTag("UI 游戏界面");
        canvas背包 = GameObject.FindGameObjectWithTag("UI 背包");
        canvas商城 = GameObject.FindGameObjectWithTag("UI 商城");
        canvas设置 = GameObject.FindGameObjectWithTag("UI 设置");
        canvas任务 = GameObject.FindGameObjectWithTag("UI 任务");
        canvas好友 = GameObject.FindGameObjectWithTag("UI 好友");
        player = GameObject.FindGameObjectWithTag("Player");
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");*/
        //camera1.SetActive(false);
        //player.SetActive(false);
        lifebar.SetActive(true);
        camera1.SetActive(true);
        canvas其他.SetActive(true);
        player.SetActive(true);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(true);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(true);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
        SQLdata qLdata = new SQLdata();
        qLdata.数据录入();
    }
    public void 返回游戏()
    {
        lifebar.SetActive(true);
        camera1.SetActive(true);
        canvas其他.SetActive(true);
        player.SetActive(true);
        //for (int i = 0; i < monster.Length; i++)
        //monster[i].SetActive(true);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(true);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
        背包 t = new 背包();t.playdata();
    }
    public void 个人信息()
    {
        camera1.SetActive(false);
        player.SetActive(false);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(false);
        canvas其他.SetActive(false);
        canvas开始.SetActive(false);
        canvas信息.SetActive(true);
        canvas界面.SetActive(false);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
        lifebar.SetActive(false);
    }
    public void 背包()
    {
        camera1.SetActive(false);
        lifebar.SetActive(false);
        player.SetActive(false);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(false);
        canvas其他.SetActive(false);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(false);
        canvas背包.SetActive(true);
        canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").gameObject.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
    }
    public void 商城()
    {
        camera1.SetActive(false);
        player.SetActive(false);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(false);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(false);
        canvas背包.SetActive(false);
        canvas商城.SetActive(true);
        canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").gameObject.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
        lifebar.SetActive(false);
        canvas其他.SetActive(false);
    }
    public void 设置()
    {
        camera1.SetActive(false);
        player.SetActive(false);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(false);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(false);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(true);
        canvas设置.transform.Find("Panel 音量控制").gameObject.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(false);
        canvas其他.SetActive(false);
        lifebar.SetActive(false);
    }
    public void 任务()
    {
        camera1.SetActive(false);
        player.SetActive(false);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(false);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(false);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(true);
        canvas好友.SetActive(false);
        canvas其他.SetActive(false);
        lifebar.SetActive(false);
    }
    public void 好友()
    {
        camera1.SetActive(false);
        player.SetActive(false);
        //for (int i = 0; i < monster.Length; i++)
            //monster[i].SetActive(false);
        canvas开始.SetActive(false);
        canvas信息.SetActive(false);
        canvas界面.SetActive(false);
        canvas背包.SetActive(false);
        canvas商城.SetActive(false);
        canvas设置.SetActive(false);
        canvas任务.SetActive(false);
        canvas好友.SetActive(true);
        lifebar.SetActive(false);
        canvas其他.SetActive(false);
    }
}
