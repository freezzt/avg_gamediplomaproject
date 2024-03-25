using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
public class 好友 : MonoBehaviour
{
    // Start is called before the first frame update
    List<Toggle> toggles = new List<Toggle>();
    public static Dictionary<string, GameObject> chatdata = new Dictionary<string, GameObject>();
    public ToggleGroup toggleGroup;
    GameObject chat;
    GameObject shuid;
    public Color selectcolor;
    public Color desecolor;
    public ColorBlock color;
    public string userfname;
    public string userfid;
    void Start()
    {
        SQLdata qLdata = new SQLdata();qLdata.friendsqldata();
        chat = transform.Find("Panel 聊天界面").gameObject;
        shuid = transform.Find("InputField (TMP) 输入玩家ID").gameObject;
        chat.SetActive(false);
        transform.Find("InputField (TMP) 输入玩家ID/Button 添加好友").gameObject.SetActive(false);
        if (transform.Find("Scroll View 好友列表/Viewport/Content").childCount >= 1)
        {
            friendtoggle();
            Toggle prefab = 游戏开始.canvas好友.transform.Find("Image 好友/Button hy1").GetComponent<Toggle>();
            selectcolor = prefab.colors.selectedColor;
            desecolor = prefab.colors.normalColor;
        }
    }
    public void friendtoggle()
    {
        foreach (Toggle toggle in transform.Find("Scroll View 好友列表/Viewport/Content").GetComponentsInChildren<Toggle>())
        {
            toggles.Add(toggle);
            toggle.group = toggleGroup;
            toggle.onValueChanged.AddListener(ontoggle);
            GameObject chatobject = Instantiate(chat, transform);
            chatobject.name = toggle.transform.parent.name;
            chatdata[toggle.transform.parent.name] = chatobject;
        }
    }
    public void ontoggle(bool ison)
    {
        chat.SetActive(false);
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (toggle.isOn)
        {
            GameObject transformchild = toggle.transform.GetChild(0).gameObject;
            //Debug.Log(transformchild.name.Length);
            chatdata[toggle.transform.parent.name].transform.Find("Image hy/Text  好友信息").GetComponent<TMP_Text>().text = toggle.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text;
            shuid.SetActive(false);
            //Task accept = network.ConnectedToServer("");
            foreach (Toggle toggle1 in toggles)
            {
                color = toggle1.colors;
                color.selectedColor = desecolor;
                color.normalColor = desecolor;
                color.highlightedColor = desecolor;
                color.pressedColor = desecolor;
                toggle1.colors = color;
                chatdata[toggle1.transform.parent.name].SetActive(false);
            }
            color = toggle.colors;
            color.selectedColor = selectcolor;
            color.normalColor = selectcolor;
            color.highlightedColor = selectcolor;
            color.pressedColor = selectcolor;
            toggle.colors = color;
            chatdata[toggle.transform.parent.name].SetActive(true);
        }
        else
        {
            color = toggle.colors;
            color.selectedColor = desecolor;
            color.normalColor = desecolor;
            color.highlightedColor = desecolor;
            color.pressedColor = desecolor;
            toggle.colors = color;
        }
    }
    public void OnDestroy()
    {
        foreach (Toggle toggle in toggles)
        {
            if (toggle != null)
            {
                toggle.onValueChanged.RemoveListener(ontoggle);
            }
        }
    }
    public void chat_返回()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        chatdata[toggle.transform.parent.name].SetActive(false);
        shuid.SetActive(true);
    }
    public void id搜索()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        transform.Find("InputField (TMP) 输入玩家ID/Button 添加好友").gameObject.SetActive(true);
        //toggle.transform.parent.name
        string str = transform.Find("InputField (TMP) 输入玩家ID").GetComponent<TMP_InputField>().text;
        SqlConnection con = new SqlConnection(SQLdata.mysqlstr);
        if (!str.Equals(SQLdata.gameuserid.Trim()))//"' and usergame_id<>" + SQLdata.gameuserid + 
        {
            con.Open();
            string sql = "SELECT [user],[usergame_id],[gamename],[exp],[hp],[atk],[def],[aspd] FROM[dbo].[dbo_游戏角色信息] where usergame_id = '" + str + "' and usergame_id not in( select friendid from dbo_好友 where gameuserid = '" + SQLdata.gameuserid + "')";
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader sqldata = com.ExecuteReader();
            TMP_Text textssjg = transform.Find("InputField (TMP) 输入玩家ID/Panel 好友/Button hy/Text (TMP)").GetComponent<TMP_Text>();
            if (sqldata.Read())
            {
                userfid = sqldata["usergame_id"].ToString();
                userfname = sqldata["gamename"].ToString();
                textssjg.text = userfid;
            }
            else
            {
                textssjg.text = "已添加/查无此人";
                transform.Find("InputField (TMP) 输入玩家ID/Button 添加好友").gameObject.SetActive(false);
            }
            con.Close();
        }
        else
        {
            TMP_Text textssjg = transform.Find("InputField (TMP) 输入玩家ID/Panel 好友/Button hy/Text (TMP)").GetComponent<TMP_Text>();
            textssjg.text = SQLdata.gameuserid;
            transform.Find("InputField (TMP) 输入玩家ID/Button 添加好友").gameObject.SetActive(false);
        }
    }
    public void 添加好友()
    {
        SqlConnection con = new SqlConnection(SQLdata.mysqlstr);
        con.Open();
        TMP_Text textssjg = transform.Find("InputField (TMP) 输入玩家ID/Panel 好友/Button hy/Text (TMP)").GetComponent<TMP_Text>();
        string sql = "INSERT INTO [dbo].[dbo_好友]([gameuserid],[friendid],[friendname],[friendstatus]) VALUES('" + SQLdata.gameuserid + "', '" + userfid + "', '" + userfname + "', 0)";
        SqlCommand command = new SqlCommand(sql, con);
        int row1 = command.ExecuteNonQuery();
        sql = "INSERT INTO [dbo].[dbo_好友]([gameuserid],[friendid],[friendname],[friendstatus]) VALUES('" + userfid + "', '" + SQLdata.gameuserid + "', '" + SQLdata.gamename + "', 0)";
        SqlCommand command1 = new SqlCommand(sql, con);
        int row2 = command1.ExecuteNonQuery();
        con.Close();
        textssjg.text = "已添加";
        destoryallchildren(transform.Find("Scroll View 好友列表/Viewport/Content"));
        SQLdata qLdata = new SQLdata(); qLdata.friendsqldata();
        chat.SetActive(false);
        transform.Find("InputField (TMP) 输入玩家ID/Button 添加好友").gameObject.SetActive(false);
        if (transform.Find("Scroll View 好友列表/Viewport/Content").childCount >= 1)
        {
            friendtoggle();
            Toggle prefab = 游戏开始.canvas好友.transform.Find("Image 好友/Button hy1").GetComponent<Toggle>();
            selectcolor = prefab.colors.selectedColor;
            desecolor = prefab.colors.normalColor;
        }
        transform.Find("InputField (TMP) 输入玩家ID/Button 添加好友").gameObject.SetActive(false);
    }
    public void destoryallchildren(Transform transform)
    {
        if (transform == null) return;
        foreach(Transform transform1 in transform)
        {
            Destroy(transform1.gameObject);
        }
    }
}