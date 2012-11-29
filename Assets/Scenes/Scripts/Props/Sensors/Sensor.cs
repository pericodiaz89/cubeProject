using UnityEngine;
using System.Collections;

public abstract class Sensor : Terrain {
    private bool isPressed = false;
    
    public bool IsPressed
    {
        get { return isPressed; }
    }
    public abstract bool CheckPressed();

    void Update() { 
        if(CheckPressed() != isPressed){
            isPressed = CheckPressed();
            Level.Singleton.NotifyChangePressed(this);
        }
    }
}
