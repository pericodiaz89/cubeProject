using UnityEngine;
using System.Collections;

public abstract class Command {
    private Cube receiver;
    private Vector3 endPosition;

    #region Observer
    private CubeController listener;

    public CubeController Listener
    {
        set { listener = value; }
    }
    
    public void EndExecution(){
        listener.CommandFinished(this);
    }

    #endregion

    public Vector3 EndPosition
    {
        get { return endPosition; }
    }

    protected Cube Cube{
        get { return receiver;}
    }

    public Command(Cube receiver, Vector3 endPosition) {
        this.receiver = receiver;
        this.endPosition = endPosition;
    }

    public abstract void Execute();
}
