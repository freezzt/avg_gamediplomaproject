using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 滚动条 : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar scrollbar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollbar.value <= 0) scrollbar.value = 0;
        else if (scrollbar.value >= 1) scrollbar.value = 1;
    }
}
