using UnityEngine;
using System.Collections;

public class NormalPushButton : Sensor{

    public override bool CheckPressed()
    {
        return Level.Singleton.Entities.ContainsKey(transform.position + transform.up.normalized);
    }
}
