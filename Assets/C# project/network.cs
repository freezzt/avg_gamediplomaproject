using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System;
using TMPro;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Linq;
using System.Threading.Tasks;
//using Unity.Plastic.Newtonsoft.Json;
using Newtonsoft.Json;
public class network : MonoBehaviour
{
    // Start is called before the first frame update
    private static readonly string SERVER_ADDRESS = "127.0.0.1";//本地服务器地址
    private static readonly int SERVER_PORT = 7777;
    private static Socket socket;
    private static byte[] buffer = new byte[1024];
    public bool isrecei = true;
    public static List<chatserver> chatlist = new List<chatserver>();
    public Task connectserver;
    public class chatclient
    {
        public string chat;
        public string id;
        public string goalid;
        public chatclient(string a,string b,string c)
        {
            chat = a;
            id = b;
            goalid = c;
        }
        public chatclient() { }
    }
    public class chatserver
    {
        public string startid;
        public string chat;
        public chatserver(string a, string b)
        {
            startid = a;
            chat = b;
        }
        public chatserver() { }
    }
    public async Task Start()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Connect(new IPEndPoint(IPAddress.Parse(SERVER_ADDRESS), SERVER_PORT));
            await connect();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private async Task connect()
    {
        await socket.ConnectAsync(new IPEndPoint(IPAddress.Parse(SERVER_ADDRESS), SERVER_PORT));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            发送();
        }
        else
            connectserver = ConnectedToServer(new chatclient(SQLdata.gamename, SQLdata.gameuserid.Trim(), "52455655563"));
        StartCoroutine(receiveserver());
        if (chatlist.Count > 0)
        {
            for (int i = 0; i < chatlist.Count; i++)
            {
                if (!chatlist[i].startid.Equals(""))
                {
                    updateuiwith(chatlist[i]);
                }
                chatlist.RemoveAt(i);
            }
        }
    }
    public void 发送()
    {
        Toggle toggle = 游戏开始.canvas好友.transform.GetComponent<好友>().toggleGroup.ActiveToggles().FirstOrDefault();
        TMP_InputField input = 游戏开始.canvas好友.transform.Find(toggle.transform.parent.name + "/InputField (TMP) 聊天输入").GetComponent<TMP_InputField>();
        if (input.text.Length >= 1)
        {
            GameObject prefab = 游戏开始.canvas好友.transform.Find("Image 聊天框").gameObject;
            Transform trancon = 游戏开始.canvas好友.transform.Find(toggle.transform.parent.name + "/Scroll View 聊天/Viewport/Content");
            GameObject chato = Instantiate(prefab, trancon);
            chato.transform.Find("Text  内容").GetComponent<TMP_Text>().text = SQLdata.gamename + ":" + input.text;
            chatclient chatstr = new chatclient(SQLdata.gamename + ":" + input.text,SQLdata.gameuserid.Trim(),toggle.transform.parent.name);
            Task task= ConnectedToServer(chatstr);//SQLdata.gamename + ":" + input.text + " " + SQLdata.gameuserid.Trim() + ">" + toggle.transform.parent.name
            input.text = "";
        }
    }
    public void updateuiwith(chatserver reply)
    {
        //string[] strlp = reply.Split(' ');
        GameObject prefab = 游戏开始.canvas好友.transform.Find("Image 聊天框").gameObject;
        Transform trancon = 游戏开始.canvas好友.transform.Find(reply.startid + "/Scroll View 聊天/Viewport/Content");
        //Debug.Log(strlp[0]);
        GameObject chato = Instantiate(prefab, trancon);
        chato.transform.GetComponent<Image>().color = Color.red;
        chato.transform.Find("Text  内容").GetComponent<TMP_Text>().text = reply.chat;
        Debug.Log("Received from server: " + reply.chat);
        
    }
    public async Task ConnectedToServer(chatclient chatstr)
    {
        //socket.Connect(new IPEndPoint(IPAddress.Parse(SERVER_ADDRESS), SERVER_PORT));
        if (socket.Connected)
        {
            string text = JsonConvert.SerializeObject(chatstr);
            byte[] messageBuffer = Encoding.UTF8.GetBytes(text);
            //Debug.Log(text);
            await sendasync(socket, messageBuffer);
        }
        else
        {
            Debug.Log("断开连接");
            await connect();
        }
    }
    public IEnumerator receiveserver()
    {
        if (socket.Connected)
        {
            Task rece=receivecallback(socket);
            //socket.BeginReceive(buffer, 0, 1024, 0, receivecallback, null);
            //Debug.Log("接收数据");
        }
        else
        {
            Debug.Log("断开连接");
            socket.Connect(new IPEndPoint(IPAddress.Parse(SERVER_ADDRESS), SERVER_PORT));
        }
        yield return null;
    }
    public async Task receivecallback(Socket a)
    {
        int count = await a.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None).ConfigureAwait(false);
        //Debug.Log("ar");
        if (count > 0)
        {
            string reply = Encoding.UTF8.GetString(buffer, 0, count);
            chatserver caeser = JsonConvert.DeserializeObject<chatserver>(reply);
            Debug.Log(reply);
            chatlist.Add(caeser);
        }
    }
    public static async Task<bool> sendasync(Socket a,byte[] messageBuffer)
    {
        await socket.SendAsync(new ArraySegment<byte>(messageBuffer), SocketFlags.None).ConfigureAwait(false);
        //Debug.Log("send");
        return true;
    }
    private void OnDestroy()
    {
        if (socket != null&&socket.Connected)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}