using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class 宝箱 : MonoBehaviour
{
    // Start is called before the first frame update
    public float dete = 3.0f;
    public float rowspeed = 100.0f;
    public float time = 0f;
    public Transform tf;
    public void Start()
    {
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, 0));
        tf = this.transform;
        transform.Find("Treasure_Chest_Up").transform.rotation = quaternion;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")&&Vector3.Distance(transform.position,other.transform.position)<=dete)
        {
            游戏开始.canvas界面.transform.Find("Button F收集").gameObject.SetActive(true);
            游戏开始.canvas界面.transform.Find("Button F收集").GetComponent<按键提示>().tf = tf;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            游戏开始.canvas界面.transform.Find("Button F收集").gameObject.SetActive(false);
        }
    }
    public void shouji()
    {
        Debug.Log(tf);
        Quaternion quaternion1 = Quaternion.Euler(new Vector3(45, 0, 0));
        Quaternion quaternion = Quaternion.Slerp(tf.Find("Treasure_Chest_Up").rotation, quaternion1, Time.deltaTime * rowspeed);
        tf.Find("Treasure_Chest_Up").rotation = quaternion;
        int lv = int.Parse(tf.name);
        SQLdata.bagdata["0001"].objectnumber += lv * 10;
        游戏开始.canvas界面.transform.Find("Image 奖励描述").gameObject.SetActive(true);
        游戏开始.canvas界面.transform.Find("Image 奖励描述/Text (TMP)").GetComponent<TMPro.TMP_Text>().text = "获得" + lv * 10 + "枚金币";
        游戏开始.canvas设置.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
        游戏开始.canvas商城.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
        游戏开始.canvas背包.transform.Find("Panel 背包/Button 金币/0001/ text 金币数").gameObject.GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
        StartCoroutine(shumiao());
        //shumiao();
    }
    IEnumerator shumiao()
    {

        while (time <= 3.0f)
        {
            yield return waittime(0.01f);
            time += Time.deltaTime;
        }
        if(time>3.0f)
        {
            游戏开始.canvas界面.transform.Find("Button F收集").gameObject.SetActive(false);
            游戏开始.canvas界面.transform.Find("Image 奖励描述").gameObject.SetActive(false);
            tf.gameObject.SetActive(false);
            游戏开始.canvas界面.transform.Find("Button F收集").GetComponent<按键提示>().y = true;
            Destroy(tf.gameObject);
        }
    }
    IEnumerator waittime(float x)
    {
        yield return new WaitForSeconds(x);
    }
}
