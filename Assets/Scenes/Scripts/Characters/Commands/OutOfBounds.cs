using UnityEngine;
using System.Collections;

public class OutOfBounds : Command {

    public OutOfBounds(Cube receiver, Vector3 endPosition) : base(receiver, endPosition) { 
    }

    public override void Execute()
    {
        //Cube.FallOutOfBounds(this,EndPosition);
    }
}
