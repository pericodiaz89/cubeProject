using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bounce : Command {
	
	private List<Vector3> bouncePositions;
		
	// Use this for initialization
	public Bounce(RubberCube receiver, Vector3 endPosition, List<Vector3> bouncePositions) : base(receiver, endPosition)
	{
		this.bouncePositions = bouncePositions;
	}

	public override void Execute ()
	{
		((RubberCube)Cube).Bounce(EndPosition,bouncePositions);
	}

	public List<Vector3> BouncePositions {
		get {
			return this.bouncePositions;
		}
		set {
			bouncePositions = value;
		}
	}
}