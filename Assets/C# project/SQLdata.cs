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
    public GameObject prefab;//Ԥ����
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
        gettagchild(transform, "Panel ��¼", list);
        panel1 = list[0];
        list = new List<GameObject>();
        gettagchild(transform, "Panel ѡ���ɫ", list);
        panel2 = list[0];
        panel1.SetActive(true);
        panel2.SetActive(false);
        transform.Find("Image ����").gameObject.SetActive(false);
        //panel1.SetActive(false);
        //panel2.SetActive(true);
        //input1 = GameObject.Find("InputField (TMP) �˺�").GetComponent<TMP_InputField>();
        List<TMP_InputField> list2 = new List<TMP_InputField>(FindObjectsOfType<TMP_InputField>());
        foreach(TMP_InputField tMP_Input in list2)
        {
            if(tMP_Input.gameObject.name.Equals("InputField (TMP) �˺�"))
            {
                input1 = tMP_Input;
            }
            if (tMP_Input.gameObject.name.Equals("InputField (TMP) ����"))
            {
                input2 = tMP_Input;
            }
        }
        //��Ϸ��ɫչʾ();
    }
    public void ��¼()
    {
        //Debug.Log(input1);
        string str1 = input1.text;
        string str2 = input2.text;
        string str3 = "";
        con.Open();
        string sql1 = "SELECT [userid],[userpassword],[username] FROM[dbo].[dbo_�˺�] where userid = " + str1;
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
            ��Ϸ��ɫչʾ();
        }
        else
        {
            panel1.SetActive(false);
            transform.Find("Image ����").gameObject.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        con.Close();
    }
    public void ȷ��()
    {
        transform.Find("Image ����").gameObject.SetActive(false);
        panel1.SetActive(true);
    }
    public void ��Ϸ��ɫչʾ()
    {
        con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_��Ϸ��ɫ��Ϣ]where [user] ='" + userid + "'";
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
            if (button.gameObject.name.Equals("Button ��ɫ1"))
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
            if (button.gameObject.name.Equals("Button ��ɫ2"))
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
    public void ��ɫѡ��()
    {
        con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_��Ϸ��ɫ��Ϣ]where[user] =" + userid;
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
            if (button.gameObject.name.Equals("Button ��ɫ1"))
            {
                button1 = button;
            }
            if (button.gameObject.name.Equals("Button ��ɫ2"))
            {
                button2 = button;
            }
        }
        sqldata.Close();
        if (button1.isOn)
        {
            gameuserid = str1;
            gamename = str2;
            ��Ϸ��ʼ t = new ��Ϸ��ʼ();
            t.gamestartclick();
            Debug.Log("1");
        }
        else if(button2.isOn)
        {
            if (!str3.Equals(""))
            {
                gameuserid = str3;
                gamename = str4;
                ��Ϸ��ʼ t = new ��Ϸ��ʼ();
                t.gamestartclick();
                Debug.Log("ashd");
            }
            else
            {
                Debug.Log("�½�ɫ");
                gameuserid = str1.Trim() + "1";
                string gamename2 = "asyfgwasafda";
                string sql2 = "INSERT INTO [dbo].[dbo_��Ϸ��ɫ��Ϣ]([user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd])VALUES('" + userid + "', '" + gameuserid + "', '" + gamename2 + "', 0, 100, 10, 10, 1)";
                SqlCommand com1 = new SqlCommand(sql2, con);
                int rows1 = com1.ExecuteNonQuery();
                string sql3 =
"INSERT INTO [dbo].[dbo_��Ʒ]  VALUES ('" + gameuserid + "','0001','���',100,'��ң��������̳ǹ�����Ʒ') " +
"INSERT INTO[dbo].[dbo_��Ʒ]  VALUES('" + gameuserid + "', '0002', '��', 10, '��ʳ���ɲ�������ֵ10%') " +
"INSERT INTO[dbo].[dbo_��Ʒ]  VALUES('" + gameuserid + "', '0003', 'ˮ��', 10, 'ˮ�����޷�ֱ��ʹ��') " +
"INSERT INTO[dbo].[dbo_��Ʒ]  VALUES('" + gameuserid + "', '0004', '����', 10, '���飬�޷�ֱ��ʹ��') " +
"INSERT INTO[dbo].[dbo_��Ʒ]  VALUES('" + gameuserid + "', '0005', 'ľ��', 10, 'ľ�ģ��޷�ֱ��ʹ��') " +
"INSERT INTO[dbo].[dbo_��Ʒ]  VALUES('" + gameuserid + "', '0006', '����ֵ����ҩ��', 10, '�ɲ���50������ֵ')";
                SqlCommand com2 = new SqlCommand(sql3, con);
                int rows2 = com2.ExecuteNonQuery();
                ��Ϸ��ʼ t = new ��Ϸ��ʼ();
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
    public void ����¼��()
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
        string sql1 = "SELECT [commodityid],[commodityname],[price],[describe] FROM[dbo].[dbo_�̳�]";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        GameObject gamemall = ��Ϸ��ʼ.canvas�̳�;
        TMP_Text text_rou = gamemall.transform.Find("Scroll View �̳�/Viewport/Content/Button ��/0002/Text (TMP) �ۼ�").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = gamemall.transform.Find("Scroll View �̳�/Viewport/Content/Button ˮ��/0003/Text (TMP) �ۼ�").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = gamemall.transform.Find("Scroll View �̳�/Viewport/Content/Button ����/0004/Text (TMP) �ۼ�").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = gamemall.transform.Find("Scroll View �̳�/Viewport/Content/Button ľ��/0005/Text (TMP) �ۼ�").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = gamemall.transform.Find("Scroll View �̳�/Viewport/Content/Button ����ֵ����ҩ��/0006/Text (TMP) �ۼ�").gameObject.GetComponent<TMP_Text>();
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
        string sql2 = "SELECT [gameuserid],[objectid],[objectname],[objectnumber],[describe]   FROM[dbo].[dbo_��Ʒ]  where gameuserid = '" + gameuserid + "'";
        SqlCommand com1 = new SqlCommand(sql2, con);
        SqlDataReader sqldata1 = com1.ExecuteReader();
        sqldata1.Read();
        GameObject cava3 = ��Ϸ��ʼ.canvas����;
        TMP_Text text_jb = cava3.transform.Find("Panel ����/Button ���/0001/ text �����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_rou = cava3.transform.Find("Panel ����/Button ��/0002/ text ����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = cava3.transform.Find("Panel ����/Button ˮ��/0003/ text ˮ����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = cava3.transform.Find("Panel ����/Button ����/0004/ text ������").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = cava3.transform.Find("Panel ����/Button ľ��/0005/ text ľ����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = cava3.transform.Find("Panel ����/Button ����ֵ����ҩ��/0006/ text ����ֵ����ҩ����").gameObject.GetComponent<TMP_Text>();
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
        string sql3 = "SELECT [monster_type],[hp],[atk],[def]FROM[dbo].[dbo_����] where monster_type = '001'";
        SqlCommand com2 = new SqlCommand(sql3, con);
        SqlDataReader sqldata2 = com2.ExecuteReader();
        sqldata2.Read();
        monster_HP = int.Parse(sqldata2["hp"].ToString());
        monster_ATK = int.Parse(sqldata2["atk"].ToString());
        monster_DEF = int.Parse(sqldata2["def"].ToString());
        sqldata2.Close();
        GameObject canxiet = GameObject.Find("Canvas ����Ѫ��").gameObject;
        int i0 = 1, i1 = 2, i2 = 3, i3 = 4, i4 = 5;
        TMP_Text LV0 = canxiet.transform.Find("Slider ����HPBarbarian/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp0 = canxiet.transform.Find("Slider ����HPBarbarian/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV1 = canxiet.transform.Find("Slider ����HPBarbarian1/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp1 = canxiet.transform.Find("Slider ����HPBarbarian1/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV2 = canxiet.transform.Find("Slider ����HPBarbarian2/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp2 = canxiet.transform.Find("Slider ����HPBarbarian2/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV3 = canxiet.transform.Find("Slider ����HPBarbarian3/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp3 = canxiet.transform.Find("Slider ����HPBarbarian3/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV4 = canxiet.transform.Find("Slider ����HPBarbarian4/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp4 = canxiet.transform.Find("Slider ����HPBarbarian4/Text (TMP) hp").GetComponent<Text>();
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
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_��Ϸ��ɫ��Ϣ]where[user] =" + userid
            + "and usergame_id='" + gameuserid + "'";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        GameObject canvas1 = ��Ϸ��ʼ.canvas����;
        TMP_Text textid = canvas1.transform.Find("Text (TMP) ID").gameObject.GetComponent<TMP_Text>();
        textid.text = "ID:" + gameuserid;
        gamename = sqldata["gamename"].ToString();
        GameObject cava2 = ��Ϸ��ʼ.canvas��Ϣ;
        TMP_Text textusername = cava2.transform.Find("ͷ��/�û���").gameObject.GetComponent<TMP_Text>();
        textusername.text = gamename;
        EXP = int.Parse(sqldata["exp"].ToString());
        HP = int.Parse(sqldata["hp"].ToString());
        ATK = int.Parse(sqldata["atk"].ToString());
        DEF = int.Parse(sqldata["def"].ToString());
        ASPD = int.Parse(sqldata["aspd"].ToString());
        js_HP = HP;
        Slider expname = cava2.transform.Find("ͷ��/������").gameObject.GetComponent<Slider>();
        int lv = 1 + EXP / 100;
        expname.value = ((EXP % 100) * 1.0f) / 100.0f;
        TMP_Text text_lv = cava2.transform.Find("ͷ��/������/�ȼ�").gameObject.GetComponent<TMP_Text>();
        Text text_exp = cava2.transform.Find("ͷ��/������/text ������ʾ").gameObject.GetComponent<Text>();
        TMP_Text text2_lv = canvas1.transform.Find("Slider ��ɫHP/image ��ɫͷ��/Text (TMP) �ȼ�/Text (TMP) LV").gameObject.GetComponent<TMP_Text>();
        Text text2_hp = canvas1.transform.Find("Slider ��ɫHP/Text (TMP) hp").gameObject.GetComponent<Text>();
        text2_lv.text = lv.ToString();
        text2_hp.text = HP.ToString() + "/" + HP.ToString();
        text_lv.text = "LV" + lv.ToString() + ".";
        text_exp.text = (EXP % 100).ToString() + "/100";
        TMP_Text text_hp = cava2.transform.Find("Panel ��ɫ����/Text (TMP) Ѫ��/Text (TMP) HP����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ATK = cava2.transform.Find("Panel ��ɫ����/Text (TMP) ������/Text (TMP) ����������").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_DEF = cava2.transform.Find("Panel ��ɫ����/Text (TMP) ������/Text (TMP) ����������").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ASPD = cava2.transform.Find("Panel ��ɫ����/Text (TMP) �����ٶ�/Text (TMP) �����ٶ�����").gameObject.GetComponent<TMP_Text>();
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
        string sql = "SELECT [gameuserid],[friendid],[friendname],[friendstatus] FROM[dbo].[dbo_����] where gameuserid = '" + gameuserid + "'";
        SqlCommand com = new SqlCommand(sql, con);
        SqlDataReader sqldata = com.ExecuteReader();
        string friendid, friendname;
        bool friendstatus;
        prefab = ��Ϸ��ʼ.canvas����.transform.Find("Image ����").gameObject;//Ԥ����
        Transform transform1 = ��Ϸ��ʼ.canvas����.transform.Find("Scroll View �����б�/Viewport/Content");
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
    public void ע��()
    {
        /*con.Open();
        string sql1 = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd]FROM[dbo_��Ϸ��ɫ��Ϣ]where[user] =" + userid
            + "and usergame_id='" + gameuserid + "'";
        SqlCommand com = new SqlCommand(sql1, con);
        SqlDataReader sqldata = com.ExecuteReader();
        sqldata.Read();
        GameObject canvas1 = ��Ϸ��ʼ.canvas����;
        TMP_Text textid = canvas1.transform.Find("Text (TMP) ID").gameObject.GetComponent<TMP_Text>();
        textid.text = "ID:" + gameuserid;
        gamename = sqldata["gamename"].ToString();
        GameObject cava2 = ��Ϸ��ʼ.canvas��Ϣ;
        TMP_Text textusername = cava2.transform.Find("ͷ��/�û���").gameObject.GetComponent<TMP_Text>();
        textusername.text = gamename;
        EXP = int.Parse(sqldata["exp"].ToString());
        HP = int.Parse(sqldata["hp"].ToString());
        ATK = int.Parse(sqldata["atk"].ToString());
        DEF = int.Parse(sqldata["def"].ToString());
        ASPD = int.Parse(sqldata["aspd"].ToString());
        js_HP = HP;
        Slider expname = cava2.transform.Find("ͷ��/������").gameObject.GetComponent<Slider>();
        int lv = 1 + EXP / 100;
        expname.value = ((EXP % 100) * 1.0f) / 100.0f;
        TMP_Text text_lv = cava2.transform.Find("ͷ��/������/�ȼ�").gameObject.GetComponent<TMP_Text>();
        Text text_exp = cava2.transform.Find("ͷ��/������/text ������ʾ").gameObject.GetComponent<Text>();
        TMP_Text text2_lv = canvas1.transform.Find("Slider ��ɫHP/image ��ɫͷ��/Text (TMP) �ȼ�/Text (TMP) LV").gameObject.GetComponent<TMP_Text>();
        Text text2_hp = canvas1.transform.Find("Slider ��ɫHP/Text (TMP) hp").gameObject.GetComponent<Text>();
        text2_lv.text = lv.ToString();
        text2_hp.text = HP.ToString() + "/" + HP.ToString();
        text_lv.text = "LV" + lv.ToString() + ".";
        text_exp.text = (EXP % 100).ToString() + "/100";
        TMP_Text text_hp = cava2.transform.Find("Panel ��ɫ����/Text (TMP) Ѫ��/Text (TMP) HP����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ATK = cava2.transform.Find("Panel ��ɫ����/Text (TMP) ������/Text (TMP) ����������").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_DEF = cava2.transform.Find("Panel ��ɫ����/Text (TMP) ������/Text (TMP) ����������").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_ASPD = cava2.transform.Find("Panel ��ɫ����/Text (TMP) �����ٶ�/Text (TMP) �����ٶ�����").gameObject.GetComponent<TMP_Text>();
        text_hp.text = HP.ToString() + "/" + HP.ToString();
        text_ATK.text = ATK.ToString();
        text_DEF.text = DEF.ToString();
        text_ASPD.text = ASPD.ToString();
        sqldata.Close();*/
        /*string sql2 = "SELECT [gameuserid],[objectid],[objectname],[objectnumber],[describe]   FROM[dbo].[dbo_��Ʒ]  where gameuserid = '" + gameuserid + "'";
        SqlCommand com1 = new SqlCommand(sql2, con);
        SqlDataReader sqldata1 = com1.ExecuteReader();
        sqldata1.Read();
        GameObject cava3 = ��Ϸ��ʼ.canvas����;
        TMP_Text text_jb = cava3.transform.Find("Panel ����/Button ���/0001/ text �����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_rou = cava3.transform.Find("Panel ����/Button ��/0002/ text ����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_sj = cava3.transform.Find("Panel ����/Button ˮ��/0003/ text ˮ����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_tk = cava3.transform.Find("Panel ����/Button ����/0004/ text ������").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_mc = cava3.transform.Find("Panel ����/Button ľ��/0005/ text ľ����").gameObject.GetComponent<TMP_Text>();
        TMP_Text text_smzbc = cava3.transform.Find("Panel ����/Button ����ֵ����ҩ��/0006/ text ����ֵ����ҩ����").gameObject.GetComponent<TMP_Text>();
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
        /*string sql3 = "SELECT [monster_type],[hp],[atk],[def]FROM[dbo].[dbo_����] where monster_type = '001'";
        SqlCommand com2 = new SqlCommand(sql3, con);
        SqlDataReader sqldata2 = com2.ExecuteReader();
        sqldata2.Read();
        monster_HP = int.Parse(sqldata2["hp"].ToString());
        monster_ATK = int.Parse(sqldata2["atk"].ToString());
        monster_DEF = int.Parse(sqldata2["def"].ToString());
        sqldata2.Close();
        GameObject canxiet = GameObject.Find("Canvas ����Ѫ��").gameObject;
        int i0 = 1, i1 = 2, i2 = 3, i3 = 4, i4 = 5;
        TMP_Text LV0 = canxiet.transform.Find("Slider ����HPBarbarian/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp0 = canxiet.transform.Find("Slider ����HPBarbarian/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV1 = canxiet.transform.Find("Slider ����HPBarbarian1/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp1 = canxiet.transform.Find("Slider ����HPBarbarian1/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV2 = canxiet.transform.Find("Slider ����HPBarbarian2/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp2 = canxiet.transform.Find("Slider ����HPBarbarian2/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV3 = canxiet.transform.Find("Slider ����HPBarbarian3/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp3 = canxiet.transform.Find("Slider ����HPBarbarian3/Text (TMP) hp").GetComponent<Text>();
        TMP_Text LV4 = canxiet.transform.Find("Slider ����HPBarbarian4/image ����/Text (TMP) �ȼ�/Text (TMP) LV").GetComponent<TMP_Text>();
        Text hp4 = canxiet.transform.Find("Slider ����HPBarbarian4/Text (TMP) hp").GetComponent<Text>();
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