using UnityEngine;
using System.Collections;

public class CubeAnimations
{
	#region Diplicator Cube Animations
	
	public static void AnimateDuplication (GameObject myGameObject)
	{
		//AnimationHelper.AnimateJump2(gameObject,Vector3.down,nextPosition,0f,"EndExecution",null);
		myGameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		AnimationHelper.AnimateGrow (myGameObject, 0f, "EndExecution", myGameObject);
		//AnimationHelper.AnimateGrow(myGameObject, 0f, "EndExecution", null);

	}
	
	#endregion
	
	#region Generic Cube Animations
	
	public static void AnimateMove (GameObject gameObject, Vector3 down, Vector3 nextPosition)
	{
		AnimationHelper.AnimateJump2 (gameObject, down, nextPosition, 0f, "EndExecution", null);
	}
	
	public static void AnimateBounce (GameObject gameObject, Vector3 down, Vector3[] nextPositions)
	{
		AnimationHelper.AnimateBounce (gameObject, down, nextPositions, 0f, "EndExecution", null);
	}
	
	#endregion
}
