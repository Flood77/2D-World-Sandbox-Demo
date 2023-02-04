using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnumUI : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public EnumData data;

    //Setup name and dropdown options based on data
    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;

            dropdown.ClearOptions();
            dropdown.AddOptions(new List<string>(data.names));
        }
    }

    //Set value and listener
    private void Start()
    {
        dropdown.value = data.index;
        dropdown.onValueChanged.AddListener(UpdateValue);
    }

    //Set data to selected value
    public void UpdateValue(int value)
    {
        data.index = value;
    }
}
