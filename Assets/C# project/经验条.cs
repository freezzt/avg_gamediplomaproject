using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 经验条 : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    public Text text;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        String[] str = text.text.Split('/');
        float st1 = Convert.ToInt32(str[0]);
        float str2 = Convert.ToInt32(str[1]);
        slider.value = st1 / str2;
    }

}
