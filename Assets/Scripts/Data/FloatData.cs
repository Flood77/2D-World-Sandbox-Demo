using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Data/Float")]
public class FloatData : ScriptableObject
{
    public float data;

    public static implicit operator float(FloatData data) { return data.data; }
}
