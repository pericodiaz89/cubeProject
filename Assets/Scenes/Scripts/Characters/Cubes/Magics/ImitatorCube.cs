using UnityEngine;
using System.Collections;

public class ImitatorCube : Cube {
	private Cube imitated;
	
	public override void MoveTo (Vector3 nextPosition)
	{
		base.MoveTo(nextPosition);
		Entity below = Level.Singleton.Entities[nextPosition + transform.up * -1.0f];
		if( below is Cube){
			Level.Singleton.Entities.Remove(transform.position);
			//imitated = gameObject.AddComponent<typeof below>();
			
		}
	}
	
	public override Command[] Options {
		get {
			if(imitated != null){
				return imitated.Options;
			}else{
				return base.Options;
			}
		}
	}
}
