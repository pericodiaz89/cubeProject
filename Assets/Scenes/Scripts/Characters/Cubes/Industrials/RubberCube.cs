using UnityEngine;
using System.Collections;

public class RubberCube : Cube {
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	public override Command[] Options {
		get {
			Command[] options = new Command[4];
			if((transform.position.y > CubeHelper.GetTopPosition(transform.position+Vector3.forward).y) 
				&& (transform.position.y >= GetNextBouncePosition(transform.position+Vector3.forward).y))
			{
                options[0] = new Bounce(this, GetNextBouncePosition(Vector3.forward), CubeHelper.GetTopPosition(transform.position+Vector3.forward));
			}
			else
			{
                options[0] = new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward));
			}

            if ((transform.position.y > CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.forward * -1.0f).y))
			{
                options[1] = new Bounce(this, GetNextBouncePosition(Vector3.forward * -1.0f), CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f));
			}
			else
			{
                options[1] = new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f));
			}

            if ((transform.position.y > CubeHelper.GetTopPosition(transform.position + Vector3.right).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.right).y))
			{
                options[2] = new Bounce(this, GetNextBouncePosition(Vector3.right), CubeHelper.GetTopPosition(transform.position + Vector3.right));
			}
			else
			{
                options[2] = new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right));
			}

            if ((transform.position.y > CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.right * -1.0f).y))
			{
                options[3] = new Bounce(this, GetNextBouncePosition(Vector3.right * -1.0f), CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f));
			}
            else
			{
                options[3] = new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f));
			}

            return options;
		}
	}
	
	private Vector3 GetNextBouncePosition(Vector3 direction)
	{
		Vector3 nextBouncePosition = transform.position + 2 * direction.normalized;
		
		bool isFreeToBounce = !Level.Singleton.Entities.ContainsKey(nextBouncePosition);
		
		if (isFreeToBounce)
        {
            while (isFreeToBounce) 
			{
                nextBouncePosition.y -= 1.0f;
                isFreeToBounce = !Level.Singleton.Entities.ContainsKey(nextBouncePosition);
                if(nextBouncePosition.y < 0)
				{
                    break;
                }
            }
            nextBouncePosition.y += 1.0f;
        }
		else return transform.position;
		
		return nextBouncePosition;
	}
	
	public  void Bounce(Vector3 nextPosition) {
        Level.Singleton.Entities.Remove(transform.position);
        transform.position = nextPosition;
        Level.Singleton.Entities.Add(transform.position, this);
    }
	
}
