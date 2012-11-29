using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    
    private static GameController singleton;
    private List<String> done, toDo;
    
    public static GameController Singleton
    {
        get
        {
            if (singleton != null)
            {
                return singleton;
            }
            else 
            {
                throw new Exception("No hay ningun GameController"); 
            }
        }
    }

    void Awake() {
        singleton = this;
        DontDestroyOnLoad(this.gameObject);
        done = new List<string>();
        toDo = new List<string>();
        //Cargar estado del juego
    }

    public void NotifyEndLevel(String s) {
        Debug.Log(s);
    }

    void OnApplicationQuit() { 
        //Guardar Estado del juego, PlayerPrefs
    }
}
