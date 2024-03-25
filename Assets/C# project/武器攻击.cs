using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class 武器攻击 : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider collider;
    public GameObject text;
    public GameObject cavans;
    public static int HP, ATK, DEF, ASPD, js_HP;
    public bool bcsh = true;
    void Start()
    {
        cavans = GameObject.Find("Canvas 其他");
        if (bcsh)
        {
            HP = SQLdata.HP;
            ATK = SQLdata.ATK;
            DEF = SQLdata.DEF;
            ASPD = SQLdata.ASPD;
            js_HP = HP;
            bcsh = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("怪物"))
        {
            applydamage(collision.gameObject);
        }
    }*/
    public void OnTriggerEnter(Collider other)
    {
        collider = other;
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("怪物")) && other.gameObject.tag != transform.root.tag)
        {
            applydamage(other.gameObject);
        }
    }
    public void applydamage(GameObject target)
    {
        //Debug.Log("碰撞");
        cavans = GameObject.Find("Canvas 其他");
        string r = "Text " + collider.name;
        //Debug.Log(r);
        //Debug.Log(cavans);
        Transform text = cavans.transform.Find(r);
        伤害数字显示 t = new 伤害数字显示(text.gameObject, 1.0f, collider.transform);
        if (transform.root.CompareTag("Player"))
        {
            //SQLdata.monsterdata[target.transform.name].def;
            t.deaidamage(Mathf.Max(ATK - SQLdata.monsterdata[target.transform.root.name].def, 0));
            Text jshp = GameObject.Find("Canvas 怪物血条/Slider 怪物HP" + target.transform.root.name + "/Text (TMP) hp").GetComponent<Text>();
            jshp.text = (SQLdata.monsterdata[target.transform.root.name].ls_hp - Mathf.Max(ATK - SQLdata.monsterdata[target.transform.root.name].def, 0)).ToString() + "/" + SQLdata.monsterdata[target.transform.root.name].hp.ToString();
            SQLdata.monsterdata[target.transform.root.name].ls_hp = SQLdata.monsterdata[target.transform.root.name].ls_hp - Mathf.Max(ATK - SQLdata.monsterdata[target.transform.root.name].def, 0);
            if (SQLdata.monsterdata[target.transform.root.name].ls_hp <= 0)
            {
                SQLdata.monsterdata[target.transform.root.name].ls_hp = 0;
                GameObject gameObject = GameObject.Find(target.transform.root.name);
                GameObject gameObject1 = GameObject.Find("Canvas 怪物血条/Slider 怪物HP" + target.transform.root.name);
                gameObject.SetActive(false);
                gameObject1.SetActive(false);
                SQLdata.EXP += SQLdata.monsterdata[target.transform.root.name].LV * 50;
                js_exp();
                怪物刷新 masd = GameObject.Find("地形").GetComponent<怪物刷新>();
                masd.刷新(0f, gameObject, gameObject1);
            }
        }
        else if (transform.root.CompareTag("怪物"))
        {
            //SQLdata.monsterdata[transform.root.name].atk
            t.deaidamage(Mathf.Max((SQLdata.monsterdata[transform.root.name].atk - DEF), 0));
            Text jshp = GameObject.Find("Canvas game interface/Slider 角色HP/Text (TMP) hp").GetComponent<Text>();
            jshp.text = (js_HP - Mathf.Max((SQLdata.monsterdata[transform.root.name].atk - DEF), 0)).ToString() + "/" + HP.ToString();
            js_HP = (int)(js_HP - Mathf.Max((SQLdata.monsterdata[transform.root.name].atk - DEF), 0));
            if (js_HP <= 0)
            {
                js_HP = HP;
                jshp.text = (js_HP - Mathf.Max((SQLdata.monsterdata[transform.root.name].atk - DEF), 0)).ToString() + "/" + HP.ToString();
            }
        }
    }
    public void js_exp()
    {
        GameObject canvas1 = 游戏开始.canvas界面;
        GameObject cava2 = 游戏开始.canvas信息;
        //int HP, ATK, DEF, ASPD, js_HP;
        Slider expname = cava2.transform.Find("头像/经验条").gameObject.GetComponent<Slider>();
        int lv = 1 + SQLdata.EXP / 100;
        HP = SQLdata.HP * lv;
        ATK = SQLdata.ATK * lv;
        DEF = SQLdata.DEF * lv;
        ASPD = SQLdata.ASPD;
        js_HP = HP;
        expname.value = ((SQLdata.EXP % 100) * 1.0f) / 100.0f;
        TMP_Text text_lv = cava2.transform.Find("头像/经验条/等级").gameObject.GetComponent<TMP_Text>();
        Text text_exp = cava2.transform.Find("头像/经验条/text 经验显示").gameObject.GetComponent<Text>();
        TMP_Text text2_lv = canvas1.transform.Find("Slider 角色HP/image 角色头像/Text (TMP) 等级/Text (TMP) LV").gameObject.GetComponent<TMP_Text>();
        Text text2_hp = canvas1.transform.Find("Slider 角色HP/Text (TMP) hp").gameObject.GetComponent<Text>();
        text2_lv.text = lv.ToString();
        text2_hp.text = HP.ToString() + "/" + HP.ToString();
        text_lv.text = "LV" + lv.ToString() + ".";
        text_exp.text = (SQLdata.EXP % 100).ToString() + "/100";
        TMP_Text text_hp = cava2.transform.Find("Panel 角色属性/Text (TMP) 血量/Text (TMP) HP数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ATK = cava2.transform.Find("Panel 角色属性/Text (TMP) 攻击力/Text (TMP) 攻击力数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_DEF = cava2.transform.Find("Panel 角色属性/Text (TMP) 防御力/Text (TMP) 防御力数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ASPD = cava2.transform.Find("Panel 角色属性/Text (TMP) 攻击速度/Text (TMP) 攻击速度数据").gameObject.GetComponent<TMP_Text>();
        text_hp.text = HP.ToString() + "/" + HP.ToString();
        text_ATK.text = ATK.ToString();
        text_DEF.text = DEF.ToString();
        text_ASPD.text = ASPD.ToString();
    }
}
