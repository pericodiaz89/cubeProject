using UnityEngine;
using System.Collections;

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
        direction.Normalize();
        position += direction;
        bool isFree = !Level.Singleton.Entities.ContainsKey(position) && !(position.x >= Level.Dimension || position.x < 0 || position.z >= Level.Dimension || position.z < 0);
        while (isFree)
        {
            position += direction;
            isFree = !Level.Singleton.Entities.ContainsKey(position) && !(position.x >= Level.Dimension || position.x < 0 || position.z >= Level.Dimension || position.z < 0);
        }
        if (Level.Singleton.Entities.ContainsKey(position))
        {
            position -= direction;
        }
        return position;
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
}
