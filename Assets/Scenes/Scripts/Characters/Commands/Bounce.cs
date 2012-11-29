using UnityEngine;
using System.Collections;

public class Bounce : Command {
	
	Vector3 intermediate;
		
	// Use this for initialization
	public Bounce(RubberCube receiver, Vector3 endPosition, Vector3 intermediate) : base(receiver, endPosition)
	{
		this.intermediate = intermediate;
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public override void Execute ()
	{
		//Cube.MoveTo(intermediate);
		((RubberCube)Cube).Bounce(EndPosition);
		EndExecution();
	}
}