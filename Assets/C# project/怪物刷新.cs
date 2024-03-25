using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 怪物刷新 : MonoBehaviour
{
    // Start is called before the first frame update
    //public float time = 0.0f;
    //public Transform transform1;
    private GameObject cachedTransform;
    private GameObject cachedTransform1;
    void Start()
    {
        //transform1 = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void 刷新(float time,GameObject transform,GameObject transform1)
    {
        if (!transform.activeSelf)
        {

            //GameObject gameObject = GameObject.Find(transform.name);
            //GameObject gameObject1 = GameObject.Find("Canvas 怪物血条/Slider 怪物HP" + transform.name);
            if (transform == null || transform1 == null)
            {
                Debug.LogError("Transform references are null. Cannot proceed with Refresh method.");
                return;
            }
            //Debug.Log(transform.name + "    " + transform1.name);
            cachedTransform = transform;
            //transform.transform.Find("Treasure_Chest").gameObject.SetActive(true);
            GameObject prefab = GameObject.Find("Treasure_Chest");
            GameObject baox = Instantiate(prefab, null);
            baox.name = SQLdata.monsterdata[transform.name].LV.ToString().Trim();
            baox.transform.position = transform.transform.position + new Vector3(0, 0.5f, 0);
            baox.GetComponent<宝箱>().tf = baox.transform;
            cachedTransform1 = transform1;
            StartCoroutine(waitandregresh(time, cachedTransform, cachedTransform1));
        }
    }
    IEnumerator waitandregresh(float time, GameObject transform, GameObject transform1)
    {
        //Debug.Log(transform.name + "34    " + transform1.name);
        while (time < 30.0f)
        {
            yield return waittime(0.01f);
            time += Time.deltaTime;
            //Debug.Log(time);
        }
        //Debug.Log(transform.name + "40    " + transform1.name);
        SQLdata.monsterdata[transform.name].ls_hp = SQLdata.monsterdata[transform.name].hp;
        Text jshp = transform1.transform.Find("Text (TMP) hp").GetComponent<Text>();
        jshp.text = (SQLdata.monsterdata[transform.name].ls_hp).ToString() + "/" + SQLdata.monsterdata[transform.name].hp.ToString();
        transform.SetActive(true);
        transform1.SetActive(true);
        time = 0f;
    }
    IEnumerator waittime(float x)
    {
        yield return new WaitForSeconds(x);
    }
}
