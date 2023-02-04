using UnityEngine;
using UnityEngine.UI;

public class FloatUI : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private Text label = null;
    [SerializeField] private Text valueText = null;

    [SerializeField] private float min = 0;
    [SerializeField] private float max = 500;
    [SerializeField] private FloatData data = null;

    //Fill in data if it exists
    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
    }

    //Set slider variables and listener
    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateValue);
        slider.maxValue = max;
        slider.minValue = min;
    }

    //Change text to resemble data
    void Update()
    {
        valueText.text = data.data.ToString();
    }

    //Change data according to slider
    void UpdateValue(float value)
    {
        data.data = value;
    }
}
