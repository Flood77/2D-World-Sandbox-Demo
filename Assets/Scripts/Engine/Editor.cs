using UnityEngine;

public class Editor : MonoBehaviour
{
    public Action[] actions;

    //Enact action on click
    public void StartAction()
    {
        actions[0].StartAction();
    }

    //Stop action on click release
    public void StopAction()
    {
        actions[0].StopAction();
    }
}
