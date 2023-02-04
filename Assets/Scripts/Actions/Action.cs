using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public enum eActionType
    {
        Creator,
        Connector
    }

    protected abstract eActionType actionType { get; }

    public abstract void StartAction();
    public abstract void StopAction();
}
