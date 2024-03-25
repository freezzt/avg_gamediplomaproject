using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
public class 背包 : MonoBehaviour
{
    List<Toggle> toggles = new List<Toggle>();
    public ToggleGroup toggleGroup;
    void Start()
    {
        foreach (Toggle toggle in transform.GetComponentsInChildren<Toggle>())
        {
            toggles.Add(toggle);
            toggle.group = toggleGroup;
            toggle.onValueChanged.AddListener((isOn) => ontoggle(isOn, toggle));
        }
        bagobjectdata();
    }
    // Update is called once per frame
    public void ontoggle(bool ison, Toggle toggle)
    {
        toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (toggle.isOn)
        {
            GameObject transformchild = toggle.transform.GetChild(0).gameObject;
            //Debug.Log(transformchild.name.Length);
            TMP_Text text = GameObject.Find("Canvas 背包/Panel 物品信息/Text (TMP) 物品信息").GetComponent<TMP_Text>();
            text.text = SQLdata.bagdata[transformchild.name].describe;
        }
    }
    public void OnDestroy()
    {
        foreach (Toggle toggle in toggles)
        {
            if (toggle != null)
            {
                toggle.onValueChanged.RemoveListener((isOn) => ontoggle(isOn, toggle));
            }
        }
    }
    public void bagbutton()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        GameObject transformchild = toggle.transform.GetChild(0).gameObject;
        switch (transformchild.name)
        {
            case "0001":
                游戏开始 t = new 游戏开始();
                t.商城();
                break;
            case "0002":
                游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").gameObject.SetActive(true);
                游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().maxValue = SQLdata.bagdata[transformchild.name].objectnumber;
                break;
            case "0006":
                游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").gameObject.SetActive(true);
                游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().maxValue = SQLdata.bagdata[transformchild.name].objectnumber;
                break;
        }
    }
    public void 确定()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        GameObject transformchild = toggle.transform.GetChild(0).gameObject;
        switch (transformchild.name)
        {
            case "0002":
                int sum = int.Parse(游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text);
                if (武器攻击.js_HP + sum * 武器攻击.HP * 0.1f <= 武器攻击.HP)
                    武器攻击.js_HP += (int)(sum * 武器攻击.HP * 0.1f);
                else
                    武器攻击.js_HP = 武器攻击.HP;
                SQLdata.bagdata[transformchild.name].objectnumber -= sum;
                游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = SQLdata.bagdata[transformchild.name].objectnumber.ToString();
                bagobjectdata();
                取消();
                break;
            case "0006":
                int su = int.Parse(游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text);
                if (武器攻击.js_HP + su * 50 <= 武器攻击.HP)
                    武器攻击.js_HP += (su * 50);
                else
                    武器攻击.js_HP = 武器攻击.HP;
                SQLdata.bagdata[transformchild.name].objectnumber -= su;
                游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = SQLdata.bagdata[transformchild.name].objectnumber.ToString();
                bagobjectdata();
                取消();
                break;
        }
    }
    public void 取消()
    {
        游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").gameObject.SetActive(false);
    }
    public void 加()
    {
        游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().value++;
        游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = 游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().value.ToString();
    }
    public void 减()
    {
        游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().value--;
        游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = 游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().value.ToString();
    }
    public void bagobjectdata()
    {
        GameObject cava3 = 游戏开始.canvas背包;
        TMP_Text text_jb = cava3.transform.Find("Panel 背包/Button 金币/0001/ text 金币数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_rou = cava3.transform.Find("Panel 背包/Button 肉/0002/ text 肉数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = cava3.transform.Find("Panel 背包/Button 水晶/0003/ text 水晶数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = cava3.transform.Find("Panel 背包/Button 铁块/0004/ text 铁块数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = cava3.transform.Find("Panel 背包/Button 木材/0005/ text 木材数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = cava3.transform.Find("Panel 背包/Button 生命值补充药剂/0006/ text 生命值补充药剂数").gameObject.GetComponent<TMP_Text>();
        text_jb.text = SQLdata.bagdata["0001"].objectnumber.ToString();
        text_rou.text = SQLdata.bagdata["0002"].objectnumber.ToString();
        text_sj.text = SQLdata.bagdata["0003"].objectnumber.ToString();
        text_tk.text = SQLdata.bagdata["0004"].objectnumber.ToString();
        text_mc.text = SQLdata.bagdata["0005"].objectnumber.ToString();
        text_smzbc.text = SQLdata.bagdata["0006"].objectnumber.ToString();
    }
    public void playdata()
    {
        Text jshp = 游戏开始.canvas界面.transform.Find("Slider 角色HP/Text (TMP) hp").GetComponent<Text>();
        jshp.text = 武器攻击.js_HP.ToString() + "/" + 武器攻击.HP.ToString();
    }
    public void sliderdata()
    {
        游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = 游戏开始.canvas背包.transform.Find("Panel 物品信息/Slider 物品使用数量").GetComponent<Slider>().value.ToString();
    }
}
