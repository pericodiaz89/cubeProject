using UnityEngine;
using System.Collections.Generic;

public class RockCube : Cube {

	public override Command[] Options{
		get{
			List<Command> commands = new List<Command>();			
			commands.AddRange(base.Options);
            for (int i = 0; i < commands.Count; i++ )
            {
                if (commands[i].EndPosition.y - transform.position.y > 0.0f)
                {
                    commands.RemoveAt(i);
                }
            }
            return commands.ToArray();
		}
	}
}
