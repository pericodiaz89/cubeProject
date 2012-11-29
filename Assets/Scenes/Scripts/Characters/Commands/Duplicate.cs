using UnityEngine;
using System.Collections;

public class Duplicate : Command {
	public Duplicate(DuplicatorCube receiver, Vector3 endPosition): base(receiver, endPosition){}
	
	public override void Execute ()
	{
		((DuplicatorCube)Cube).Duplicate(EndPosition);
        //EndExecution();
	}
}
