using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class StringUI : MonoBehaviour
{
    public TMP_Text label = null;

    public StringData data = null;

    void Update()
    {
        label.text = data.data;
    }
}
