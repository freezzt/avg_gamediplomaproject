using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class 按钮选中 : MonoBehaviour
{
    // Start is called before the first frame update
    List<Toggle> toggles = new List<Toggle>();
    public Color selectcolor;
    public Color desecolor;
    public ColorBlock color;
    public ToggleGroup toggleGroup;
    void Start()
    {
        foreach(Toggle toggle in transform.GetComponentsInChildren<Toggle>())
        {
            toggles.Add(toggle);
            toggle.group = toggleGroup;
            toggle.onValueChanged.AddListener((isOn) => ontoggle(isOn, toggle));
        }
        selectcolor = toggles[0].colors.selectedColor;
        desecolor = toggles[0].colors.normalColor;
    }

    // Update is called once per frame
    public void ontoggle(bool ison,Toggle toggle)
    {

        /*if (toggleGroup.ActiveToggles().FirstOrDefault() != null)
            toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        else
        {
            toggles[0].isOn = true;
            toggle = toggles[0];
        }
        //toggle.isOn = true;*/
        toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (toggle.isOn)
        {
            foreach (Toggle toggle1 in toggles)
            {
                color = toggle1.colors;
                color.selectedColor = desecolor;
                color.normalColor = desecolor;
                color.highlightedColor = desecolor;
                color.pressedColor = desecolor;
                toggle1.colors = color;
            }
            color = toggle.colors;
            color.selectedColor = selectcolor;
            color.normalColor = selectcolor;
            color.highlightedColor = selectcolor;
            color.pressedColor = selectcolor;
            toggle.colors = color;
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
            if(toggle!=null)
            {
                toggle.onValueChanged.RemoveListener((isOn) => ontoggle(isOn, toggle));
            }
        }
    }

}
