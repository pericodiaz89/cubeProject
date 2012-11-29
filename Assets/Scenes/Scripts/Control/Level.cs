using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Level : MonoBehaviour
{
    private Dictionary<Vector3, Entity> entities;
    private static int dimension = 10;

    public static int Dimension
    {
        get { return Level.dimension; }
    }

    public Dictionary<Vector3, Entity> Entities
    {
        get {return entities; }
    }

    private ArrayList sensors;
    private static Level singleton;

    public static Level Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameObject("Level").AddComponent<Level>();
            }
            return singleton;
        }
    }

    public bool IsFinished
    {
        get
        {
            bool isFinished = true;
            foreach (Sensor s in sensors)
            {
                if (!s.CheckPressed())
                {
                    isFinished = false;
                    break;
                }
            }
            return isFinished;
        }
    }

    void Awake()
    {
        entities = new Dictionary<Vector3, Entity>(new Vector3EqualityComparer());
        sensors = new ArrayList();
    }

    void OnDestroy()
    {
        singleton = null;
    }

    public void AddEntity(Entity e, Vector3 position)
    {
        entities.Add(position, e);
        if (e is Sensor)
        {
            sensors.Add(e);
        }
    }

    public void NotifyChangePressed(Sensor s)
    {
        if (s.IsPressed)
        {
            sensors.Remove(s);
            if(sensors.Count == 0){
                GameController.Singleton.NotifyEndLevel(Application.loadedLevelName);
            }
        }
        else
        { 
            if(!sensors.Contains(s)){
                sensors.Add(s);
            }
        }
    }
	
	/*void Update(){
		foreach(Vector3 v in entities.Keys){
			if(entities[v] is Cube)
			{
				Debug.Log(v);
			}
		}
		
	}*/
}
