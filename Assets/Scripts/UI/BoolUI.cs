using UnityEngine;
using UnityEngine.UI;



public class BoolUI : MonoBehaviour
{
    [SerializeField] private Toggle toggle = null;
    [SerializeField] private BoolData data = null;
    [SerializeField] private Text label = null;


    //Set text based on data
    private void OnValidate()
    {
        if (data != null)
        {
            name = data.name;
            label.text = name;
        }
    }

    //Create listener
    private void Start()
    {
        toggle.onValueChanged.AddListener(UpdateValue);
    }
    
    //Change toggle based on data
    void Update()
    {
        toggle.isOn = data.data;
    }

    //Update data based on toggle click
    void UpdateValue(bool value)
    {
        data.data = value;
    }
}
