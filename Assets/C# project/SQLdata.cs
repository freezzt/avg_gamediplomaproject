using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;
using System.Data.SqlClient;
using UnityEngine.SceneManagement;
public class bagobject
{
    public string objectname;
    public int objectnumber;
    public string describe;
    public bagobject(string x,int y,string z)
    {
        objectname = x;
        objectnumber = y;
        describe = z;
    }
}
public class mallobject
{
    public string objectname;
    public int price;
    public string describe;
    public mallobject(string x, int y, string z)
    {
        objectname = x;
        price = y;
        describe = z;
    }
}
public class friendobject
{
    public string friendid;
    public string friendname;
    public bool friendstatus;
    public friendobject(string x,string y,bool z)
    {
        friendid = x;
        friendname = y;
        friendstatus = z;
    }
}
public class monsterobject
{
    public int LV;
    public float hp;
    public float ls_hp;
    public float atk;
    public float def;
    public monsterobject(int x0, float x,float y,float z)
    {
        LV = x0;
        hp = x;
        atk = y;
        def = z;
        ls_hp = hp;
    }
}
public class SQLdata : MonoBehaviour
{
    // Start is called before the first frame update
    public static string mysqlstr = "Data Source=LAPTOP-A9O4VA5E;Initial Catalog=AVGgame1;Integrated Security=True";
    public TMP_InputField input1, input2;
    public GameObject panel1, panel2;
    public GameObject prefab;//预制体
    public SqlConnection con = new SqlConnection(mysqlstr);
    public static string userid, gameuserid, gamename;
    public static int EXP, HP, ATK, DEF, ASPD, js_HP;
    public static int monster_HP, monster_ATK, monster_DEF;
    public static Dictionary<string, bagobject> bagdata = new Dictionary<string, bagobject>();
    public static Dictionary<string, monsterobject> monsterdata = new Dictionary<string, monsterobject>();
    public static Dictionary<string, mallobject> malldata = new Dictionary<string, mallobject>();
    public static Dictionary<string, friendobject> frienddata = new Dictionary<string, friendobject>();
    void Start()
    {
        List<GameObject> list = new List<GameObject>();
        gettagchild(transform, "Panel 登录", list);
        panel1 = list[0];
        list = new List<GameObject>();
        gettagchild(transform, "Panel 选择角色", list);
        panel2 = list[0];
        panel1.SetActive(true);
        panel2.SetActive(false);
        transform.Find("Image 错误").gameObject.SetActive(false);
        //panel1.SetActive(false);
        //panel2.SetActive(true);
        //input1 = GameObject.Find("InputField (TMP) 账号").GetComponent<TMP_InputField>();
        List<TMP_InputField> list2 = new List<TMP_InputField>(FindObjectsOfType<TMP_InputField>());
        foreach(TMP_InputField tMP_Input in list2)
        {
            if(tMP_Input.gameObject.name.Equals("InputField (TMP) 账号"))
            {
                input1 = tMP_Input;
            }
            if (tMP_Input.gameObject.name.Equals("InputField (TMP) 密码"))
            {
                input2 = tMP_Input;
            }
        }
        //游戏角色展示();
    }
    public void 登录()
    {
        //Debug.Log(input1);
        string str1 = input1.text;
        string str2 = input2.text;
        string str3 = "";
        con.Open();
        string sql1 = "SELECT [userid],[userpassword],[username] FROM[dbo].[dbo_账号] where userid = " + str1;
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        if(sqldata.Read())
        str3 = sqldata["userpassword"].ToString();
        //Debug.Log(str2 + "    " + str3);
        if (str3.Equals(str2))
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            userid = sqldata["userid"].ToString();
            con.Close();
            游戏角色展示();
        }
        else
        {
            panel1.SetActive(false);
            transform.Find("Image 错误").gameObject.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        con.Close();
    }
    public void 确定()
    {
        transform.Find("Image 错误").gameObject.SetActive(false);
        panel1.SetActive(true);
    }
    public void 游戏角色展示()
    {
        con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_游戏角色信息]where [user] ='" + userid + "'";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        string str1 = sqldata["usergame_id"].ToString();
        string str2 = sqldata["gamename"].ToString();
        int exp1 = (int)sqldata["exp"];
        //Debug.Log(str2 + "    " + str3);
        Toggle button1, button2;
        TMP_Text text11, text12, text21, text22;
        foreach(Toggle button in panel2.transform.GetComponentsInChildren<Toggle>())
        {
            if (button.gameObject.name.Equals("Button 角色1"))
            {
                button1 = button;
                button1.isOn = true;
                foreach (TMP_Text texttmp in button1.transform.GetComponentsInChildren<TMP_Text>())
                {
                    if (texttmp.gameObject.name.Equals("Text (TMP)"))
                    {
                        text11 = texttmp;
                        text11.text = str2 + "  LV." + (1 + exp1 / 100).ToString();
                    }
                    if (texttmp.gameObject.name.Equals("Text (TMP) ID"))
                    {
                        text12 = texttmp;
                        text12.text = str1;
                    }
                }
            }
            if (button.gameObject.name.Equals("Button 角色2"))
            {
                button2 = button;
                if (sqldata.Read())
                {
                    string str3 = sqldata["usergame_id"].ToString();
                    string str4 = sqldata["gamename"].ToString();
                    int exp2 = (int)sqldata["exp"];
                    foreach (TMP_Text texttmp in button2.transform.GetComponentsInChildren<TMP_Text>())
                    {
                        if (texttmp.gameObject.name.Equals("Text (TMP)"))
                        {
                            text21 = texttmp;
                            text21.text = str4 + "  LV." + (1 + exp2 / 100).ToString();
                        }
                        if (texttmp.gameObject.name.Equals("Text (TMP) ID"))
                        {
                            text22 = texttmp;
                            text22.text = str3;
                        }
                    }
                }
            }
        }
        sqldata.Close();
        con.Close();
    }
    public void 角色选择()
    {
        con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_游戏角色信息]where[user] =" + userid;
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        string str1 = sqldata["usergame_id"].ToString();
        string str2 = sqldata["gamename"].ToString();
        string str3 = "", str4 = "";
        if (sqldata.Read())
        {
            str3 = sqldata["usergame_id"].ToString();
            str4 = sqldata["gamename"].ToString();
        }
        Toggle button1, button2;
        button1 = GetComponent<Toggle>();
        button2 = GetComponent<Toggle>();
        foreach (Toggle button in panel2.transform.GetComponentsInChildren<Toggle>())
        {
            if (button.gameObject.name.Equals("Button 角色1"))
            {
                button1 = button;
            }
            if (button.gameObject.name.Equals("Button 角色2"))
            {
                button2 = button;
            }
        }
        sqldata.Close();
        if (button1.isOn)
        {
            gameuserid = str1;
            gamename = str2;
            游戏开始 t = new 游戏开始();
            t.gamestartclick();
            Debug.Log("1");
        }
        else if(button2.isOn)
        {
            if (!str3.Equals(""))
            {
                gameuserid = str3;
                gamename = str4;
                游戏开始 t = new 游戏开始();
                t.gamestartclick();
                Debug.Log("ashd");
            }
            else
            {
                Debug.Log("新角色");
                gameuserid = str1.Trim() + "1";
                string gamename2 = "asyfgwasafda";
                string sql2 = "INSERT INTO [dbo].[dbo_游戏角色信息]([user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd])VALUES('" + userid + "', '" + gameuserid + "', '" + gamename2 + "', 0, 100, 10, 10, 1)";
                SqlCommand com1 = new SqlCommand(sql2, con);
                int rows1 = com1.ExecuteNonQuery();
                string sql3 =
"INSERT INTO [dbo].[dbo_物品]  VALUES ('" + gameuserid + "','0001','金币',100,'金币，可用于商城购买物品') " +
"INSERT INTO[dbo].[dbo_物品]  VALUES('" + gameuserid + "', '0002', '肉', 10, '肉食，可补充生命值10%') " +
"INSERT INTO[dbo].[dbo_物品]  VALUES('" + gameuserid + "', '0003', '水晶', 10, '水晶，无法直接使用') " +
"INSERT INTO[dbo].[dbo_物品]  VALUES('" + gameuserid + "', '0004', '铁块', 10, '铁块，无法直接使用') " +
"INSERT INTO[dbo].[dbo_物品]  VALUES('" + gameuserid + "', '0005', '木材', 10, '木材，无法直接使用') " +
"INSERT INTO[dbo].[dbo_物品]  VALUES('" + gameuserid + "', '0006', '生命值补充药剂', 10, '可补充50点生命值')";
                SqlCommand com2 = new SqlCommand(sql3, con);
                int rows2 = com2.ExecuteNonQuery();
                游戏开始 t = new 游戏开始();
                t.gamestartclick();
            }
        }
        con.Close();
    }
    public void gettagchild(Transform parent, string tagname, List<GameObject> list_y)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.name.Equals(tagname))
            {
                list_y.Add(child.gameObject);
            }
            gettagchild(child, tagname, list_y);
        }
    }
    public void 数据录入()
    {
        playersqldata();
        bagsqldata();
        monstersqldata();
        mallsqldata();
        //friendsqldata();
    }
    public void mallsqldata()
    {
        con.Open();
        string sql1 = "SELECT [commodityid],[commodityname],[price],[describe] FROM[dbo].[dbo_商城]";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        GameObject gamemall = 游戏开始.canvas商城;
        TMP_Text text_rou = gamemall.transform.Find("Scroll View 商城/Viewport/Content/Button 肉/0002/Text (TMP) 售价").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = gamemall.transform.Find("Scroll View 商城/Viewport/Content/Button 水晶/0003/Text (TMP) 售价").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = gamemall.transform.Find("Scroll View 商城/Viewport/Content/Button 铁块/0004/Text (TMP) 售价").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = gamemall.transform.Find("Scroll View 商城/Viewport/Content/Button 木材/0005/Text (TMP) 售价").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = gamemall.transform.Find("Scroll View 商城/Viewport/Content/Button 生命值补充药剂/0006/Text (TMP) 售价").gameObject.GetComponent<TMP_Text>();
        string commodityid = sqldata["commodityid"].ToString().Trim();
        string commodityname = sqldata["commodityname"].ToString();
        int price = int.Parse(sqldata["price"].ToString());
        string describe = sqldata["describe"].ToString();
        mallobject mallobject2 = new mallobject(commodityname, price, describe);
        malldata[commodityid] = mallobject2;
        sqldata.Read();
        commodityid = sqldata["commodityid"].ToString().Trim();
        commodityname = sqldata["commodityname"].ToString();
        price = int.Parse(sqldata["price"].ToString());
        describe = sqldata["describe"].ToString();
        mallobject mallobject3 = new mallobject(commodityname, price, describe);
        malldata[commodityid] = mallobject3;
        sqldata.Read();
        commodityid = sqldata["commodityid"].ToString().Trim();
        commodityname = sqldata["commodityname"].ToString();
        price = int.Parse(sqldata["price"].ToString());
        describe = sqldata["describe"].ToString();
        mallobject mallobject4 = new mallobject(commodityname, price, describe);
        malldata[commodityid] = mallobject4;
        sqldata.Read();
        commodityid = sqldata["commodityid"].ToString().Trim();
        commodityname = sqldata["commodityname"].ToString();
        price = int.Parse(sqldata["price"].ToString());
        describe = sqldata["describe"].ToString();
        mallobject mallobject5 = new mallobject(commodityname, price, describe);
        malldata[commodityid] = mallobject5;
        sqldata.Read();
        commodityid = sqldata["commodityid"].ToString().Trim();
        commodityname = sqldata["commodityname"].ToString();
        price = int.Parse(sqldata["price"].ToString());
        describe = sqldata["describe"].ToString();
        mallobject mallobject6 = new mallobject(commodityname, price, describe);
        malldata[commodityid] = mallobject6;
        sqldata.Close();
        text_rou.text = mallobject2.price.ToString();
        text_sj.text = mallobject3.price.ToString();
        text_tk.text = mallobject4.price.ToString();
        text_mc.text = mallobject5.price.ToString();
        text_smzbc.text = mallobject6.price.ToString();
        con.Close();
    }
    public void bagsqldata()
    {
        con.Open();
        string sql2 = "SELECT [gameuserid],[objectid],[objectname],[objectnumber],[describe]   FROM[dbo].[dbo_物品]  where gameuserid = '" + gameuserid + "'";
        SqlCommand com1 = new SqlCommand(sql2, con);
        SqlDataReader sqldata1 = com1.ExecuteReader();
        sqldata1.Read();
        GameObject cava3 = 游戏开始.canvas背包;
        TMP_Text text_jb = cava3.transform.Find("Panel 背包/Button 金币/0001/ text 金币数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_rou = cava3.transform.Find("Panel 背包/Button 肉/0002/ text 肉数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = cava3.transform.Find("Panel 背包/Button 水晶/0003/ text 水晶数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = cava3.transform.Find("Panel 背包/Button 铁块/0004/ text 铁块数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = cava3.transform.Find("Panel 背包/Button 木材/0005/ text 木材数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = cava3.transform.Find("Panel 背包/Button 生命值补充药剂/0006/ text 生命值补充药剂数").gameObject.GetComponent<TMP_Text>();
        string objectid = sqldata1["objectid"].ToString().Trim();
        string objectname = sqldata1["objectname"].ToString();
        int objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        string describe = sqldata1["describe"].ToString();
        bagobject bagobject1 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject1;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject2 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject2;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject3 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject3;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject4 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject4;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject5 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject5;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject6 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject6;
        text_jb.text = bagobject1.objectnumber.ToString();
        text_rou.text = bagobject2.objectnumber.ToString();
        text_sj.text = bagobject3.objectnumber.ToString();
        text_tk.text = bagobject4.objectnumber.ToString();
        text_mc.text = bagobject5.objectnumber.ToString();
        text_smzbc.text = bagobject6.objectnumber.ToString();
        sqldata1.Close();
        con.Close();
    }
    public void monstersqldata()
    {
        con.Open();
        string sql3 = "SELECT [monster_type],[hp],[atk],[def]FROM[dbo].[dbo_怪物] where monster_type = '001'";
        SqlCommand com2 = new SqlCommand(sql3, con);
        SqlDataReader sqldata2 = com2.ExecuteReader();
        sqldata2.Read();
        monster_HP = int.Parse(sqldata2["hp"].ToString());
        monster_ATK = int.Parse(sqldata2["atk"].ToString());
        monster_DEF = int.Parse(sqldata2["def"].ToString());
        sqldata2.Close();
        GameObject canxiet = GameObject.Find("Canvas 怪物血条").gameObject;
        int i0 = 1, i1 = 2, i2 = 3, i3 = 4, i4 = 5;
        TMP_Text LV0 = canxiet.transform.Find("Slider 怪物HPBarbarian/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp0 = canxiet.transform.Find("Slider 怪物HPBarbarian/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV1 = canxiet.transform.Find("Slider 怪物HPBarbarian1/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp1 = canxiet.transform.Find("Slider 怪物HPBarbarian1/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV2 = canxiet.transform.Find("Slider 怪物HPBarbarian2/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp2 = canxiet.transform.Find("Slider 怪物HPBarbarian2/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV3 = canxiet.transform.Find("Slider 怪物HPBarbarian3/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp3 = canxiet.transform.Find("Slider 怪物HPBarbarian3/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV4 = canxiet.transform.Find("Slider 怪物HPBarbarian4/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp4 = canxiet.transform.Find("Slider 怪物HPBarbarian4/Text (TMP) hp").GetComponent<Text>();
        monsterobject Barbarian = new monsterobject(i0, monster_HP * i0, monster_ATK * i0, monster_DEF * i0);
        monsterobject Barbarian1 = new monsterobject(i1, monster_HP * i1, monster_ATK * i1, monster_DEF * i1);
        monsterobject Barbarian2 = new monsterobject(i2, monster_HP * i2, monster_ATK * i2, monster_DEF * i2);
        monsterobject Barbarian3 = new monsterobject(i3, monster_HP * i3, monster_ATK * i3, monster_DEF * i3);
        monsterobject Barbarian4 = new monsterobject(i4, monster_HP * i4, monster_ATK * i4, monster_DEF * i4);
        monsterdata["Barbarian"] = Barbarian;
        monsterdata["Barbarian1"] = Barbarian1;
        monsterdata["Barbarian2"] = Barbarian2;
        monsterdata["Barbarian3"] = Barbarian3;
        monsterdata["Barbarian4"] = Barbarian4;
        LV0.text = Barbarian.LV.ToString();
        hp0.text = Barbarian.hp.ToString() + "/" + Barbarian.hp.ToString();
        LV1.text = Barbarian1.LV.ToString();
        hp1.text = Barbarian1.hp.ToString() + "/" + Barbarian1.hp.ToString();
        LV2.text = Barbarian2.LV.ToString();
        hp2.text = Barbarian2.hp.ToString() + "/" + Barbarian2.hp.ToString();
        LV3.text = Barbarian3.LV.ToString();
        hp3.text = Barbarian3.hp.ToString() + "/" + Barbarian3.hp.ToString();
        LV4.text = Barbarian4.LV.ToString();
        hp4.text = Barbarian4.hp.ToString() + "/" + Barbarian4.hp.ToString();
        con.Close();
    }
    public void playersqldata()
    {
        con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_游戏角色信息]where[user] =" + userid
            + "and usergame_id='" + gameuserid + "'";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        GameObject canvas1 = 游戏开始.canvas界面;
        TMP_Text textid = canvas1.transform.Find("Text (TMP) ID").gameObject.GetComponent<TMP_Text>();
        textid.text = "ID:" + gameuserid;
        gamename = sqldata["gamename"].ToString();
        GameObject cava2 = 游戏开始.canvas信息;
        TMP_Text textusername = cava2.transform.Find("头像/用户名").gameObject.GetComponent<TMP_Text>();
        textusername.text = gamename;
        EXP = int.Parse(sqldata["exp"].ToString());
        HP = int.Parse(sqldata["hp"].ToString());
        ATK = int.Parse(sqldata["atk"].ToString());
        DEF = int.Parse(sqldata["def"].ToString());
        ASPD = int.Parse(sqldata["aspd"].ToString());
        js_HP = HP;
        Slider expname = cava2.transform.Find("头像/经验条").gameObject.GetComponent<Slider>();
        int lv = 1 + EXP / 100;
        expname.value = ((EXP % 100) * 1.0f) / 100.0f;
        TMP_Text text_lv = cava2.transform.Find("头像/经验条/等级").gameObject.GetComponent<TMP_Text>();
        Text text_exp = cava2.transform.Find("头像/经验条/text 经验显示").gameObject.GetComponent<Text>();
        TMP_Text text2_lv = canvas1.transform.Find("Slider 角色HP/image 角色头像/Text (TMP) 等级/Text (TMP) LV").gameObject.GetComponent<TMP_Text>();
        Text text2_hp = canvas1.transform.Find("Slider 角色HP/Text (TMP) hp").gameObject.GetComponent<Text>();
        text2_lv.text = lv.ToString();
        text2_hp.text = HP.ToString() + "/" + HP.ToString();
        text_lv.text = "LV" + lv.ToString() + ".";
        text_exp.text = (EXP % 100).ToString() + "/100";
        TMP_Text text_hp = cava2.transform.Find("Panel 角色属性/Text (TMP) 血量/Text (TMP) HP数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ATK = cava2.transform.Find("Panel 角色属性/Text (TMP) 攻击力/Text (TMP) 攻击力数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_DEF = cava2.transform.Find("Panel 角色属性/Text (TMP) 防御力/Text (TMP) 防御力数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ASPD = cava2.transform.Find("Panel 角色属性/Text (TMP) 攻击速度/Text (TMP) 攻击速度数据").gameObject.GetComponent<TMP_Text>();
        text_hp.text = HP.ToString() + "/" + HP.ToString();
        text_ATK.text = ATK.ToString();
        text_DEF.text = DEF.ToString();
        text_ASPD.text = ASPD.ToString();
        sqldata.Close();
        con.Close();
    }
    public void friendsqldata()
    {
        con.Open();
        string sql = "SELECT [gameuserid],[friendid],[friendname],[friendstatus] FROM[dbo].[dbo_好友] where gameuserid = '" + gameuserid + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader sqldata = com.ExecuteReader();
        string friendid, friendname;
        bool friendstatus;
        prefab = 游戏开始.canvas好友.transform.Find("Image 好友").gameObject;//预制体
        Transform transform1 = 游戏开始.canvas好友.transform.Find("Scroll View 好友列表/Viewport/Content");
        while(sqldata.Read())
        {
            friendid = sqldata["friendid"].ToString();
            friendname = sqldata["friendname"].ToString();
            friendstatus = sqldata["friendstatus"].ToString().Equals("true");
            friendobject friendobject = new friendobject(friendid, friendname, friendstatus);
            frienddata[friendid] = friendobject;
            GameObject friendo = Instantiate(prefab, transform1);
            TMP_Text tMP_Text = friendo.transform.Find("Button hy1/Text (TMP)").GetComponent<TMP_Text>();
            tMP_Text.text = friendname;
            friendo.name = friendid.Trim();
        }
        con.Close();
    }
    public void 注释()
    {
        /*con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_游戏角色信息]where[user] =" + userid
            + "and usergame_id='" + gameuserid + "'";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        GameObject canvas1 = 游戏开始.canvas界面;
        TMP_Text textid = canvas1.transform.Find("Text (TMP) ID").gameObject.GetComponent<TMP_Text>();
        textid.text = "ID:" + gameuserid;
        gamename = sqldata["gamename"].ToString();
        GameObject cava2 = 游戏开始.canvas信息;
        TMP_Text textusername = cava2.transform.Find("头像/用户名").gameObject.GetComponent<TMP_Text>();
        textusername.text = gamename;
        EXP = int.Parse(sqldata["exp"].ToString());
        HP = int.Parse(sqldata["hp"].ToString());
        ATK = int.Parse(sqldata["atk"].ToString());
        DEF = int.Parse(sqldata["def"].ToString());
        ASPD = int.Parse(sqldata["aspd"].ToString());
        js_HP = HP;
        Slider expname = cava2.transform.Find("头像/经验条").gameObject.GetComponent<Slider>();
        int lv = 1 + EXP / 100;
        expname.value = ((EXP % 100) * 1.0f) / 100.0f;
        TMP_Text text_lv = cava2.transform.Find("头像/经验条/等级").gameObject.GetComponent<TMP_Text>();
        Text text_exp = cava2.transform.Find("头像/经验条/text 经验显示").gameObject.GetComponent<Text>();
        TMP_Text text2_lv = canvas1.transform.Find("Slider 角色HP/image 角色头像/Text (TMP) 等级/Text (TMP) LV").gameObject.GetComponent<TMP_Text>();
        Text text2_hp = canvas1.transform.Find("Slider 角色HP/Text (TMP) hp").gameObject.GetComponent<Text>();
        text2_lv.text = lv.ToString();
        text2_hp.text = HP.ToString() + "/" + HP.ToString();
        text_lv.text = "LV" + lv.ToString() + ".";
        text_exp.text = (EXP % 100).ToString() + "/100";
        TMP_Text text_hp = cava2.transform.Find("Panel 角色属性/Text (TMP) 血量/Text (TMP) HP数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ATK = cava2.transform.Find("Panel 角色属性/Text (TMP) 攻击力/Text (TMP) 攻击力数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_DEF = cava2.transform.Find("Panel 角色属性/Text (TMP) 防御力/Text (TMP) 防御力数据").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ASPD = cava2.transform.Find("Panel 角色属性/Text (TMP) 攻击速度/Text (TMP) 攻击速度数据").gameObject.GetComponent<TMP_Text>();
        text_hp.text = HP.ToString() + "/" + HP.ToString();
        text_ATK.text = ATK.ToString();
        text_DEF.text = DEF.ToString();
        text_ASPD.text = ASPD.ToString();
        sqldata.Close();*/
        /*string sql2 = "SELECT [gameuserid],[objectid],[objectname],[objectnumber],[describe]   FROM[dbo].[dbo_物品]  where gameuserid = '" + gameuserid + "'";
        SqlCommand com1 = new SqlCommand(sql2, con);
        SqlDataReader sqldata1 = com1.ExecuteReader();
        sqldata1.Read();
        GameObject cava3 = 游戏开始.canvas背包;
        TMP_Text text_jb = cava3.transform.Find("Panel 背包/Button 金币/0001/ text 金币数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_rou = cava3.transform.Find("Panel 背包/Button 肉/0002/ text 肉数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = cava3.transform.Find("Panel 背包/Button 水晶/0003/ text 水晶数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = cava3.transform.Find("Panel 背包/Button 铁块/0004/ text 铁块数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = cava3.transform.Find("Panel 背包/Button 木材/0005/ text 木材数").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = cava3.transform.Find("Panel 背包/Button 生命值补充药剂/0006/ text 生命值补充药剂数").gameObject.GetComponent<TMP_Text>();
        string objectid = sqldata1["objectid"].ToString().Trim();
        string objectname = sqldata1["objectname"].ToString();
        int  objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        string describe = sqldata1["describe"].ToString();
        bagobject bagobject1 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject1;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject2 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject2;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject3 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject3;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject4 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject4;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject5 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject5;
        sqldata1.Read();
        objectid = sqldata1["objectid"].ToString().Trim();
        objectname = sqldata1["objectname"].ToString();
        objectnumber = int.Parse(sqldata1["objectnumber"].ToString());
        describe = sqldata1["describe"].ToString();
        bagobject bagobject6 = new bagobject(objectname, objectnumber, describe);
        bagdata[objectid] = bagobject6;
        text_jb.text = bagobject1.objectnumber.ToString();
        text_rou.text = bagobject2.objectnumber.ToString();
        text_sj.text = bagobject3.objectnumber.ToString();
        text_tk.text = bagobject4.objectnumber.ToString();
        text_mc.text = bagobject5.objectnumber.ToString();
        text_smzbc.text = bagobject6.objectnumber.ToString();
        sqldata1.Close();*/
        /*string sql3 = "SELECT [monster_type],[hp],[atk],[def]FROM[dbo].[dbo_怪物] where monster_type = '001'";
        SqlCommand com2 = new SqlCommand(sql3, con);
        SqlDataReader sqldata2 = com2.ExecuteReader();
        sqldata2.Read();
        monster_HP = int.Parse(sqldata2["hp"].ToString());
        monster_ATK = int.Parse(sqldata2["atk"].ToString());
        monster_DEF = int.Parse(sqldata2["def"].ToString());
        sqldata2.Close();
        GameObject canxiet = GameObject.Find("Canvas 怪物血条").gameObject;
        int i0 = 1, i1 = 2, i2 = 3, i3 = 4, i4 = 5;
        TMP_Text LV0 = canxiet.transform.Find("Slider 怪物HPBarbarian/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp0 = canxiet.transform.Find("Slider 怪物HPBarbarian/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV1 = canxiet.transform.Find("Slider 怪物HPBarbarian1/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp1 = canxiet.transform.Find("Slider 怪物HPBarbarian1/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV2 = canxiet.transform.Find("Slider 怪物HPBarbarian2/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp2 = canxiet.transform.Find("Slider 怪物HPBarbarian2/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV3 = canxiet.transform.Find("Slider 怪物HPBarbarian3/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp3 = canxiet.transform.Find("Slider 怪物HPBarbarian3/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV4 = canxiet.transform.Find("Slider 怪物HPBarbarian4/image 怪物/Text (TMP) 等级/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp4 = canxiet.transform.Find("Slider 怪物HPBarbarian4/Text (TMP) hp").GetComponent<Text>();
        monsterobject Barbarian = new monsterobject(i0, monster_HP * i0, monster_ATK * i0, monster_DEF * i0);
        monsterobject Barbarian1 = new monsterobject(i1, monster_HP * i1, monster_ATK * i1, monster_DEF * i1);
        monsterobject Barbarian2 = new monsterobject(i2, monster_HP * i2, monster_ATK * i2, monster_DEF * i2);
        monsterobject Barbarian3 = new monsterobject(i3, monster_HP * i3, monster_ATK * i3, monster_DEF * i3);
        monsterobject Barbarian4 = new monsterobject(i4, monster_HP * i4, monster_ATK * i4, monster_DEF * i4);
        monsterdata["Barbarian"] = Barbarian;
        monsterdata["Barbarian1"] = Barbarian1;
        monsterdata["Barbarian2"] = Barbarian2;
        monsterdata["Barbarian3"] = Barbarian3;
        monsterdata["Barbarian4"] = Barbarian4;
        LV0.text = Barbarian.LV.ToString();
        hp0.text = Barbarian.hp.ToString() + "/" + Barbarian.hp.ToString();
        LV1.text = Barbarian1.LV.ToString();
        hp1.text = Barbarian1.hp.ToString() + "/" + Barbarian1.hp.ToString();
        LV2.text = Barbarian2.LV.ToString();
        hp2.text = Barbarian2.hp.ToString() + "/" + Barbarian2.hp.ToString();
        LV3.text = Barbarian3.LV.ToString();
        hp3.text = Barbarian3.hp.ToString() + "/" + Barbarian3.hp.ToString();
        LV4.text = Barbarian4.LV.ToString();
        hp4.text = Barbarian4.hp.ToString() + "/" + Barbarian4.hp.ToString();
        con.Close();*/
    }
}