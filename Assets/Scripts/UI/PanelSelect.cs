using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelect : MonoBehaviour
{
    [System.Serializable]
    public struct PanelInfo
    {
        public GameObject panel;
        public Button button;
        public KeyCode keyCode;
    }

    public KeyCode toggleKey;
    public GameObject master;
    public PanelInfo[] panelInfos;

    void Start()
    {
        foreach(var panelInfo in panelInfos)
        {
            panelInfo.button.onClick.AddListener(delegate { ButtonEvents(panelInfo); });
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            master.SetActive(!master.activeSelf);
        }

        foreach(var panelInfo in panelInfos)
        {
            if (Input.GetKeyDown(panelInfo.keyCode))
            {
                SetPanelActive(panelInfo);
            }
        }
    }

    void SetPanelActive(PanelInfo panelInfo)
    {
        for(int i = 0; i < panelInfos.Length; i++)
        {
            bool active = panelInfos[i].Equals(panelInfo);
            panelInfos[i].panel.SetActive(active);
        }
    }

    void ButtonEvents(PanelInfo panelInfo)
    {
        SetPanelActive(panelInfo);
    }
}
