using UnityEngine;
using TMPro;



public class StringUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label = null;
    [SerializeField] private StringData data = null;

    //Update text with given data
    void Update()
    {
        label.text = data.data;
    }
}
