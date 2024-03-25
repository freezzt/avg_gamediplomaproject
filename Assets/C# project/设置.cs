using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class 设置 : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audio;//声音混合器
    void Start()
    {
        游戏开始.canvas设置.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
    }
    public void jb()
    {
        SQLdata.bagdata["0001"].objectnumber += 100;
        游戏开始.canvas设置.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
        游戏开始.canvas商城.transform.Find("金币/ text 金币数").GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
        游戏开始.canvas背包.transform.Find("Panel 背包/Button 金币/0001/ text 金币数").gameObject.GetComponent<TMP_Text>().text = SQLdata.bagdata["0001"].objectnumber.ToString();
    }
    public void volume()
    {
        游戏开始.canvas设置.transform.Find("Panel 音量控制").gameObject.SetActive(true);
    }
    public void gameexit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /*游戏开始.lifebar.SetActive(false);
        游戏开始.canvas其他.SetActive(false);
        游戏开始.camera1.SetActive(false);
        游戏开始.player.SetActive(false);
        游戏开始.canvas开始.SetActive(true);
        游戏开始.canvas信息.SetActive(false);
        游戏开始.canvas界面.SetActive(false);
        游戏开始.canvas背包.SetActive(false);
        游戏开始.canvas商城.SetActive(false);
        游戏开始.canvas设置.SetActive(false);
        游戏开始.canvas任务.SetActive(false);
        游戏开始.canvas好友.SetActive(false);*/
    }
    public void master()
    {
        游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 总音量/Text (TMP) 音量").GetComponent<TMP_Text>().text = 游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 总音量").GetComponent<Slider>().value.ToString();
    }
    public void bgm()
    {
        游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 背景音量/Text (TMP) 音量").GetComponent<TMP_Text>().text = 游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 背景音量").GetComponent<Slider>().value.ToString();
    }
    public void texiao()
    {
        游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 特效音量/Text (TMP) 音量").GetComponent<TMP_Text>().text = 游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 特效音量").GetComponent<Slider>().value.ToString();
    }
    public void 确定()
    {
        audio.SetFloat("main", 游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 总音量").GetComponent<Slider>().value);
        audio.SetFloat("bgm", 游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 背景音量").GetComponent<Slider>().value);
        audio.SetFloat("texiao", 游戏开始.canvas设置.transform.Find("Panel 音量控制/Slider 特效音量").GetComponent<Slider>().value);
        游戏开始.canvas设置.transform.Find("Panel 音量控制").gameObject.SetActive(false);
    }
}
