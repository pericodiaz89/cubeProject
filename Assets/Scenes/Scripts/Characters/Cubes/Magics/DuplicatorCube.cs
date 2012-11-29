using UnityEngine;
using System.Collections.Generic;

public class DuplicatorCube : Cube {
	public Object clone;
    public int duplicateTimes;
	
	public override Command[] Options {
		get {
		    List<Command> commands = new List<Command>();
			commands.AddRange(base.Options);
			if(duplicateTimes>0){
                commands.Add(new Duplicate(this, transform.position + Vector3.up));
            }
            return commands.ToArray();
		}
	}
	
	public void Duplicate(Vector3 newPosition){
		GameObject newClone = (GameObject) Instantiate(clone, newPosition, transform.rotation);
		CubeAnimations.AnimateDuplication(newClone);
        duplicateTimes--;
	}
}
