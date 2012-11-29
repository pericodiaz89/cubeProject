using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour,IClickable {

    private CubeControllerInput listener;

    public CubeControllerInput Listener
    {
        set { listener = value; }
    }

    public void NotifyClick()
    {
        listener.NotifySelection(this);
    }
}
