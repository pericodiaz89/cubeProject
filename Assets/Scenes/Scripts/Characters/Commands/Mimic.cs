using UnityEngine;
using System.Collections;

public class Mimic : Command {

	// Use this for initialization
	private TwinCube twin;
	private Vector3 direction;
	
	public Mimic(TwinCube receiver, TwinCube twin, Vector3 endPosition,Vector3 direction): base(receiver,endPosition ){
		this.twin = twin;
		this.direction = direction;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void Execute ()
	{
		((TwinCube)Cube).Mimic(EndPosition,direction);
		EndExecution();
	}
}