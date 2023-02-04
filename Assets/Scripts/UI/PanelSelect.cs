using UnityEngine;
using UnityEngine.UI;

public class PanelSelect : MonoBehaviour
{
    [System.Serializable]
    private struct PanelInfo
    {
        public GameObject panel;
        public Button button;
        public KeyCode keyCode;
    }

    [SerializeField] private KeyCode toggleKey;
    [SerializeField] private GameObject master;
    [SerializeField] private PanelInfo[] panelInfos;

    //Add Listener event for each panel button
    void Start()
    {
        foreach(var panelInfo in panelInfos)
        {
            panelInfo.button.onClick.AddListener(delegate { SetPanelActive(panelInfo); });
        }
    }

    void Update()
    {
        //Hide/Unhide panels
        if (Input.GetKeyDown(toggleKey))
        {
            master.SetActive(!master.activeSelf);
        }

        //Set panel active if coresponding key is pressed
        foreach(var panelInfo in panelInfos)
        {
            if (Input.GetKeyDown(panelInfo.keyCode))
            {
                SetPanelActive(panelInfo);
            }
        }
    }

    //Set given panel active
    void SetPanelActive(PanelInfo panelInfo)
    {
        //Set matching panel to active and disable non-matches
        for(int i = 0; i < panelInfos.Length; i++)
        {
            bool active = panelInfos[i].Equals(panelInfo);
            panelInfos[i].panel.SetActive(active);
        }
    }
}
