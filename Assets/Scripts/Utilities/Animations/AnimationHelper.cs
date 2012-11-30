using UnityEngine;
using System.Collections;

public class AnimationHelper : MonoBehaviour
{ //TODO quitar Monobehavior
	
	public static float offset = 0.4f;
	public static GameObject originTemp; // Its used to change the origin of the gameObject
	public static GameObject rotationGameObject;
	
	// Bounce Animation variables
	public static GameObject bounceObject;
	public static Vector3 down;
	public static Vector3[] nextPositions;
	public static int actualPosition;
	public static float delay;
	public static string onCompleteMethod;
	public static object parameters;
	
	void Start ()
	{
		//float delay = AnimateShrink(gameObject, 0f);
		//AnimateGrow(gameObject, delay);
		
//		Vector3[] testPositions = {gameObject.transform.position + new Vector3(1,-1,0),
//		gameObject.transform.position + new Vector3(1,-1,0) + new Vector3(1,-3,0),
//		gameObject.transform.position + new Vector3(1,-1,0) + new Vector3(1,-3,0) + new Vector3(1,-1,0)};
//		
//		AnimateBounce(gameObject, 
//			Vector3.down,
//			testPositions, 
//			0.0f);
	}
	
	private static Hashtable getBasicHs (Vector3 amount, float time, float delay, iTween.EaseType easeType)
	{
		Hashtable hs = new Hashtable ();
		hs.Add ("amount", amount);
		hs.Add ("time", time);
		hs.Add ("delay", delay);
		hs.Add ("space", Space.World);
		hs.Add ("easetype", easeType.ToString ());
		return hs;
	}
	
	#region Animate Bounce
	
	public static void AnimateBounce (GameObject gameObject, Vector3 down, Vector3[] nextPositions, float delay)
	{
		
		AnimateBounce (gameObject, down, nextPositions, delay, null, null);
	}
	
	public static void AnimateBounce (GameObject gameObject, Vector3 down, Vector3[] nextPositions, float delay, 
		string onCompleteMethod, object parameters)
	{
		
		AnimationHelper.actualPosition = 0;
		AnimationHelper.nextPositions = nextPositions;
		AnimationHelper.delay = delay;
		AnimationHelper.down = down;
		AnimationHelper.bounceObject = gameObject;
		AnimationHelper.onCompleteMethod = onCompleteMethod;
		AnimationHelper.parameters = parameters;
		
		AnimateJump2 (bounceObject, down, nextPositions [actualPosition], delay, "AnimateBouncePositions", null);
	}
	
	/// <summary>
	/// Animates each bounce position (using nextPositions) recursivelly.
	/// </summary>
	public void AnimateBouncePositions ()
	{
		actualPosition++;
		if (actualPosition < nextPositions.Length - 1) {// The last last position should make the recursive call
			AnimateJump2 (bounceObject, down, nextPositions [actualPosition], delay, "AnimateBouncePositions", parameters);
		} else {// This is the last movement
			AnimateJump2 (bounceObject, down, nextPositions [actualPosition], delay, onCompleteMethod, parameters);
		}
	}
	
	#endregion
	
	#region Animate Size
	
	public static float AnimateShrink (GameObject gameObject, float delay)
	{
		return AnimateShrink (gameObject, delay, null, null);
	}
	
	public static float AnimateShrink (GameObject gameObject, float delay, string onCompleteMethod, object parameters)
	{			
		
		Hashtable hs = new Hashtable ();
		
		hs.Add ("scale", new Vector3 (0.01f, 0.01f, 0.01f));
		hs.Add ("delay", delay);
		hs.Add ("time", 1.0f);
		hs.Add ("easetype", iTween.EaseType.easeInBack);
		
		if (onCompleteMethod != null && onCompleteMethod.Length > 0) {
			hs.Add ("onComplete", onCompleteMethod);
			hs.Add ("onCompleteTarget", gameObject);
			if (parameters != null) {
				hs.Add ("oncompleteparams", parameters);
			}
		}
		iTween.ScaleTo (gameObject, hs);
		
		delay += 1.0f;
		
		return delay;
	}
	
	public static float AnimateGrow (GameObject gameObject, float delay)
	{
		return AnimateGrow (gameObject, delay, null, null);
	}
	
	public static float AnimateGrow (GameObject gameObject, float delay, string onCompleteMethod, object parameters)
	{	
		
		Hashtable hs = new Hashtable ();
		
		hs.Add ("scale", new Vector3 (1.0f, 1.0f, 1.0f));
		hs.Add ("delay", delay);
		hs.Add ("time", 0.5f);
		hs.Add ("easetype", iTween.EaseType.spring);
		
		if (onCompleteMethod != null && onCompleteMethod.Length > 0) {
			hs.Add ("onComplete", onCompleteMethod);
			hs.Add ("onCompleteTarget", gameObject);
			if (parameters != null) {
				hs.Add ("oncompleteparams", parameters);
			}
		}
		iTween.ScaleTo (gameObject, hs);
		
		delay += 0.5f;
		
		return delay;
	}
		
	#endregion
	
	#region Animate Jumps

	public static float AnimateJump (GameObject objective, Vector3 down, Vector3 finalPosition, float delay)
	{
		return AnimateJump (objective, down, finalPosition, delay, null, null);
	}
	
	/// <summary>
	/// Animate
	/// </summary>
	/// <returns>
	/// Time spend on animation plus delay
	/// </returns>
	/// <param>
	/// <param name='objective'>
	/// GameObject to
	/// </param>
	/// <param name='down'>
	/// Down unitary movement
	/// </param>
	/// <param name='finalPosition'>
	/// Final position.
	/// </param>
	/// </param>
	/// <param name='delay'>
	/// Delay of the animation
	/// </param>
	///

	public static float AnimateJump (GameObject objective, Vector3 down, Vector3 finalPosition, float delay, 
		string onCompleteMethod, object parameters)
	{
        
		Vector3 vertical = Vector3.zero;
		Vector3 finalVertical = Vector3.zero;
		Vector3 finalMovement = finalPosition - objective.transform.position;
		Vector3 directionAxis = -down;// movement is countrary to down
		Vector3 upMovement = Vector3.Dot (directionAxis, finalMovement) * directionAxis;
		Vector3 vectorOffset = (offset * directionAxis);
		Vector3 sideMovement = (finalMovement - upMovement);
		Vector3 rotate = Vector3.Cross (sideMovement.normalized, down) * 90;
		
		if (Vector3.Dot (down, upMovement) < 0) {
			// Jumping
			vertical = vectorOffset + upMovement;
			finalVertical = -vectorOffset;
		} else if (Vector3.Dot (down, upMovement) > 0) {
			// Going Down
			vertical = vectorOffset;
			finalVertical = upMovement - vertical;
		} else {
			// Moving to the side
			vertical = vectorOffset;
			finalVertical = -vertical;
		}
		
		Hashtable hs = getBasicHs (vertical, 0.5f, delay, iTween.EaseType.spring);
		iTween.MoveAdd (objective, hs);
		delay += 0.5f;
		
		hs = getBasicHs (sideMovement, 0.5f, delay, iTween.EaseType.spring);
		iTween.MoveAdd (objective, hs);
		delay += 0.5f;
		
		hs = getBasicHs (rotate, 0.3f, delay, iTween.EaseType.spring);
		iTween.RotateAdd (objective, hs);
		delay += 0.3f;
		
		hs = getBasicHs (finalVertical, 0.5f, delay, iTween.EaseType.easeInBack);
		if (onCompleteMethod != null && onCompleteMethod.Length > 0) {
			hs.Add ("onComplete", onCompleteMethod);
			hs.Add ("onCompleteTarget", objective);
			if (parameters != null) {
				hs.Add ("oncompleteparams", parameters);
			}
		}
		iTween.MoveAdd (objective, hs);
		delay += 0.5f;
		
		return delay;
	}
	
	public static float AnimateJump2 (GameObject objective, Vector3 down, Vector3 finalPosition, float delay)
	{
		return AnimateJump2 (objective, down, finalPosition, delay, null, null);
	}
	
	/// <summary>
	/// Animates the jump2.
	/// </summary>
	/// <param name='objective'>
	/// Objective. GameObject that will be animated
	/// </param>
	/// <param name='down'>
	/// Down. The direction where the cube is stepping on
	/// </param>
	/// <param name='finalPosition'>
	/// Final position. Where the cube wants to end up
	/// </param>
	/// <param name='delay'>
	/// Delay. The time delay if you are connecting more than one animation
	/// </param>
	/// <param name='onCompleteMethod'>
	/// On complete method. If you want to continue or add logic after an animation
	/// place the name of the method on this parameter, if you dont just leave it null or blank.
	/// </param>
	public static float AnimateJump2 (GameObject objective, Vector3 down, Vector3 finalPosition, float delay, 
		string onCompleteMethod, object parameters)
	{
		
		Vector3 finalMovement = finalPosition - objective.transform.position;
		Vector3 directionAxis = -down;// movement is countrary to down
		Vector3 upMovement = Vector3.Dot (directionAxis, finalMovement) * directionAxis;// MoveDirection*Quantity + offset
		Vector3 sideDirection = (finalMovement - upMovement).normalized;
		Vector3 rotationAxis = Vector3.Cross (sideDirection, down) * 90;
		Vector3 sideMovement = finalMovement - upMovement;
		
		// Sets the new Origin to rotate around an specific Axis - originTemp
		originTemp = new GameObject ("temp");
		originTemp.transform.position = objective.transform.position + (down + sideDirection) / 2f;
		objective.transform.parent = originTemp.transform;
		rotationGameObject = objective;
		
		Hashtable hs;
		
		if (Vector3.Dot (down, upMovement) < 0) {
			// Jumps
			hs = getBasicHs (upMovement, 0.5f, delay, iTween.EaseType.spring);
			iTween.MoveAdd (originTemp, hs);
			delay = delay + 0.5f;
		}
		// Rotates
		hs = getBasicHs (rotationAxis, 0.5f, delay, iTween.EaseType.spring);
		hs.Add ("onComplete", "RotationFinished");
		hs.Add ("onCompleteTarget", originTemp);
		iTween.RotateAdd (originTemp, hs);
		delay = delay + 0.5f;
		
		if (Vector3.Dot (down, upMovement) > 0) {
			// Goes Down
			hs = getBasicHs (upMovement, 0.5f, delay, iTween.EaseType.spring);
			iTween.MoveAdd (objective, hs);
			delay = delay + 0.5f;
		}
		if (onCompleteMethod != null && onCompleteMethod.Length > 0) {
			hs = getBasicHs (Vector3.zero, 0.0f, delay, iTween.EaseType.spring);
			hs.Add ("onComplete", onCompleteMethod);
			hs.Add ("onCompleteTarget", objective);
			if (parameters != null) {
				hs.Add ("oncompleteparams", parameters);
			}
			iTween.MoveAdd (objective, hs);
		}
		
		return delay;
	}
	
	/// <summary>
	/// Method that is called once the originTemp is finished rotating
	/// The idea is to remove rotationGameObject from its parent originTemp
	/// to bring it back to its normal state
	/// </summary>
	public void RotationFinished ()
	{
		if (originTemp != null) {
			rotationGameObject.transform.parent = null;
			rotationGameObject = null;
			MonoBehaviour.Destroy (originTemp);
		}
	}

	#endregion
	
	#region Animate Slide
	
	/// <summary>
	/// Animates the slide movement of a cube.
	/// </summary>
	/// <returns>
	/// Time spent on animation plus delay
	/// </returns>
	/// <param name='gameObject'>
	/// Game object of the cube.
	/// </param>
	/// <param name='finalPosition'>
	/// Final position of the slide.
	/// </param>
	/// <param name='delay'>
	/// Total Delay. It takes into acount the delay parameter
	/// </param>
	public static float AnimateSlide (GameObject gameObject, Vector3 finalPosition, float delay)
	{
		Vector3 direction = finalPosition - gameObject.transform.position;
		float time = direction.magnitude * 0.2f;
		Hashtable hs = getBasicHs (direction, time, delay, iTween.EaseType.spring);
		iTween.MoveAdd (gameObject, hs);
		return delay + time;
	}
	
	#endregion
}
