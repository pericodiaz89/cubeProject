using UnityEngine;
using System.Collections.Generic;

public class Cube : Entity, IClickable{
	
    private bool isSelected = false;
    private static Cube selectedCube;
	public Command command;

	public Command Command {
		get {
			return this.command;
		}
		set {
			command = value;
		}
	}
	
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

    protected override void Start() {
        base.Start();
    }

    public virtual void MoveTo(Vector3 nextPosition) {
        Level.Singleton.Entities.Remove(transform.position);
		CubeAnimations.AnimateMove(gameObject, Vector3.down, nextPosition);
        Level.Singleton.Entities.Add(nextPosition, this);
    }
	
	public void EndExecution(){
		OrganizeTransform();
		if(command != null){
			command.EndExecution();
		}
	}
	
	private Vector3 Vector3Round(Vector3 v){
		return new Vector3(Mathf.Round(v.x),Mathf.Round(v.y),Mathf.Round(v.z));
	}
	
	public void OrganizeTransform(){
		Transform obj = gameObject.transform.parent;
		transform.parent = null;
		if (obj != null){
			MonoBehaviour.Destroy(obj.gameObject);
		}
		transform.position = Vector3Round(transform.position);
		transform.rotation = Quaternion.Euler(Vector3Round(transform.rotation.eulerAngles));
	}
	
    public virtual Command[] Options{ 
        get {
            List<Command> options = new List<Command>();
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position+Vector3.forward)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f)));

            for (int i = 0; i < options.Count; i++ )
            {
                if(options[i].EndPosition.y - transform.position.y > 1)
                {
                    options.RemoveAt(i);
                }
            }

            return options.ToArray();
        }
    }

    public void FallOutOfBounds(Command command,Vector3 outOfBouncePosition)
    {
		AnimationHelper.AnimateSlide(gameObject,outOfBouncePosition + new Vector3(0,-10,0),1f,"KillCube",null);
    }
	
	public void KillCube(){
		Destroy(gameObject);
	}

    public void NotifyClick()
    {
        if (selectedCube != this)
        {
            if(selectedCube != null){
                selectedCube.IsSelected = false;
            }
            isSelected = true;
            selectedCube = this;
        }
    }
}
