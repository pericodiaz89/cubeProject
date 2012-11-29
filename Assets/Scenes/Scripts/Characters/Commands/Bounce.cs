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
		//Cube.MoveTo(intermediate);
		//Cube.MoveTo(EndPosition);
		//AnimationHelper.AnimateBounce(Cube,Vector3.down,bouncePosition,EndPosition);
		((RubberCube)Cube).Bounce(EndPosition,bouncePositions);
		//EndExecution();
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