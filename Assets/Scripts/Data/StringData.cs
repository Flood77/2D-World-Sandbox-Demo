using UnityEngine;

[CreateAssetMenu(fileName = "String", menuName = "Data/String")]
public class StringData : ScriptableObject
{
    public string data;

    public static implicit operator string(StringData data) { return data.data; }
}
