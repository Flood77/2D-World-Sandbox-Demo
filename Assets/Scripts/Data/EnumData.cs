using UnityEngine;

public abstract class EnumData : ScriptableObject
{
    public abstract int index { get; set; }
    public abstract string[] names { get; }
}

