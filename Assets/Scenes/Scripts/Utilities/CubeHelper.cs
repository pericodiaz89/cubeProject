using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeHelper {
    public static Vector3 GetTopPosition(Vector3 position)
    {
        bool isFree = !Level.Singleton.Entities.ContainsKey(position);
        if (!isFree)
        {
            while (!isFree)
            {
                position.y += 1.0f;
                isFree = !Level.Singleton.Entities.ContainsKey(position);
            }
        }
        else
        {
            while (isFree)
            {
                position.y -= 1.0f;
                isFree = !Level.Singleton.Entities.ContainsKey(position);
                if (position.y < 0)
                {
                    break;
                }
            }
            position.y += 1.0f;
        }

        return position;
    }
    public static bool IsFree(Vector3 position) {
        return !Level.Singleton.Entities.ContainsKey(position);
    }
    public static Vector3 GetLastPositionInDirection(Vector3 position, Vector3 direction) {
		int diff = GetDifferenceInDirection(position,direction.normalized);
		if (diff > 0){
			return position + (direction * (diff - 1));
		}else{
			throw new UnityException("No encontro cubo en esa direccion: " + direction);
		}
//        direction.Normalize();
//        position += direction;
//        bool isFree = !Level.Singleton.Entities.ContainsKey(position) && !(position.x >= Level.Dimension || position.x < 0 || position.z >= Level.Dimension || position.z < 0);
//        while (isFree)
//        {
//            position += direction;
//            isFree = !Level.Singleton.Entities.ContainsKey(position) && !(position.x >= Level.Dimension || position.x < 0 || position.z >= Level.Dimension || position.z < 0);
//        }
//        if (Level.Singleton.Entities.ContainsKey(position))
//        {
//            position -= direction;
//        }
//        return position;
    }
    public static Entity GetEntityInPosition(Vector3 position) {
        if(Level.Singleton.Entities.ContainsKey(position))
        {
            return Level.Singleton.Entities[position];
        }
        else
        {
            return null;
        }
        
    }
	
	public static int GetDifferenceInDirection(Vector3 position, Vector3 direction){
		direction = direction.normalized;
		List<Vector3> inColumn = new List<Vector3>();
		Vector3 valueFinder =  VectorHelper.BinaryInverse(direction);
		valueFinder = VectorHelper.Abs(valueFinder);
		Vector3 columnPosition =  VectorHelper.Multiply(position,valueFinder);
		Vector3 valueGetter = VectorHelper.Abs(direction);
		float min = -1;
		foreach (Vector3 key in Level.Singleton.Entities.Keys){
			if ( !VectorHelper.Eq(position,key)
				&&  VectorHelper.Eq(VectorHelper.Multiply(key, valueFinder),columnPosition)){
				Vector3 directionTemp = key - position;
				float diff = Vector3.Dot( direction, directionTemp);
				if (diff > 0 ){
					if (min == -1){
						min = diff;
					}else {
						min = Mathf.Min(min,diff);
					}
				}
			}
		}
		return Mathf.RoundToInt(min);
	}
}
