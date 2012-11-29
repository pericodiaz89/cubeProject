using UnityEngine;
using System.Collections.Generic;

public class CubeControllerInput : CubeController {
    private Dictionary<Selector, Command> commands;
    public Object selectorCube;
    private bool initedSelectors = false;

    protected override void Awake()
    {
        base.Awake();
        commands = new Dictionary<Selector, Command>();
    }

    public override void CommandFinished(Command command)
    {
        base.CommandFinished(command);
        if (Cube.IsSelected)
        {
            UpdateSelectors();
        }
    }

    private void UpdateSelectors()
    {
        foreach(Selector s in commands.Keys){
            Destroy(s.gameObject);
        }
        commands.Clear();
        foreach(Command c in Cube.Options){
            GameObject selectorGameObject = (GameObject)Instantiate(selectorCube);
            Selector selector = selectorGameObject.AddComponent<Selector>();
            selectorGameObject.name = "Selector" + gameObject.name;
            selector.Listener = this;
            selectorGameObject.transform.position = c.EndPosition;
            commands.Add(selector, c);
        }
		Debug.Log(Cube.transform.position);
    }

    void OnDestroy() { 
        foreach(Selector s in commands.Keys){
            if(s != null){
                Destroy(s.gameObject);
            }
        }
    }

    protected override void Update()
    {
        if (Cube.IsSelected)
        {
            if(!initedSelectors){
                UpdateSelectors();
                initedSelectors = true;
            }
        }
        else 
        {
            if(commands.Count > 0){
                foreach (Selector s in commands.Keys)
                {
                    Destroy(s.gameObject);
                }
                commands.Clear();
                initedSelectors = false;
            }
        }
        base.Update();
    }

    public void NotifySelection(Selector selector)
    {
        AddCommand(commands[selector]);
        UpdateSelectors();
    }
}
