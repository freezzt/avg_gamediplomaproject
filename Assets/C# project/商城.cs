using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class 商城 : MonoBehaviour
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
        游戏开始.canvas商城.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
    }
    // Update is called once per frame
    public void ontoggle(bool ison, Toggle toggle)
    {
        toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (toggle.isOn)
        {
            GameObject transformchild = toggle.transform.GetChild(0).gameObject;
            Debug.Log(transformchild.name.Length);
            TMP_Text text = GameObject.Find("Canvas 商城/Panel 物品信息/Text (TMP) 物品信息").GetComponent<TMP_Text>();
            text.text = SQLdata.malldata[transformchild.name].describe;
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
    public void mallbutton()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        GameObject transformchild = toggle.transform.GetChild(0).gameObject;
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").gameObject.SetActive(true);
        int maxprice = SQLdata.bagdata["0001"].objectnumber;
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").GetComponent<Slider>().maxValue = (maxprice / SQLdata.malldata[transformchild.name].price);
    }
    public void 确定()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        GameObject transformchild = toggle.transform.GetChild(0).gameObject;
        int sum = int.Parse(游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量/Text (TMP) 数量").GetComponent<TMP_Text>().text);
        if (SQLdata.bagdata["0001"].objectnumber >= (SQLdata.malldata[transformchild.name].price * sum))
        {
            SQLdata.bagdata["0001"].objectnumber = SQLdata.bagdata["0001"].objectnumber - SQLdata.malldata[transformchild.name].price * sum;
            SQLdata.bagdata[transformchild.name].objectnumber += sum;
            背包 t = new 背包(); t.bagobjectdata();
            游戏开始.canvas商城.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
            取消();
        }
        else
        {
            取消();
        }
    }
    public void 取消()
    {
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").gameObject.SetActive(false);
    }
    public void 加()
    {
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").GetComponent<Slider>().value++;
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = 游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").GetComponent<Slider>().value.ToString();
    }
    public void 减()
    {
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").GetComponent<Slider>().value--;
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = 游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").GetComponent<Slider>().value.ToString();
    }
    public void sliderdata()
    {
        游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量/Text (TMP) 数量").GetComponent<TMP_Text>().text = 游戏开始.canvas商城.transform.Find("Panel 物品信息/Slider 物品购买数量").GetComponent<Slider>().value.ToString();
    }
}
