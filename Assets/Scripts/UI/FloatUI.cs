using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FloatUI : MonoBehaviour
{
    public Slider slider = null;
    public Text label = null;
    public Text valueText = null;

    public float min = 0;
    public float max = 500;
    public FloatData data = null;

    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateValue);
        slider.maxValue = max;
        slider.minValue = min;
    }

    void Update()
    {
        valueText.text = data.data.ToString();
    }

    void UpdateValue(float value)
    {
        data.data = value;
    }
}
