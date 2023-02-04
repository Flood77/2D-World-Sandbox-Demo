using UnityEngine;

[CreateAssetMenu(fileName = "Bool", menuName = "Data/Bool")]
public class BoolData : ScriptableObject
{
    public bool data;

    public static implicit operator bool(BoolData data) { return data.data; }
}
