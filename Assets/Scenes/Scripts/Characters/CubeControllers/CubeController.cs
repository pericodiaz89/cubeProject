using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public abstract class CubeController : MonoBehaviour
{

    #region Atributtes
    private Cube cube;
    private Command currentCommand;

    public Command CurrentCommand
    {
        get { return currentCommand; }
    }

    public Cube Cube
    {
        get { return cube; }
    }
    private Queue<Command> commandQueue;

    #endregion

    protected virtual void Awake() {
        commandQueue = new Queue<Command>();
        cube = gameObject.GetComponent<Cube>();
    }

    protected virtual void Update()
    {
        if (currentCommand == null && commandQueue.Count > 0 && cube.IsSelected)
        {
			Command c = commandQueue.Dequeue();
			cube.Command = c;
            ExecuteCommand(c);
        }
    }

    protected void AddCommand(Command c) {
        commandQueue.Enqueue(c);
    }

    private void ExecuteCommand(Command command)
    {
        currentCommand = command;
        command.Listener = this;
        command.Execute();
    }
    
    public virtual void CommandFinished(Command command)
    {
        if (command.Equals(currentCommand))
        {
            currentCommand = null;
        }
        else
        {
            throw new Exception("El comando finalizado no era el actual");
        }
    }
}
