using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Vector3EqualityComparer : IEqualityComparer<Vector3>{

    public bool Equals(Vector3 v1, Vector3 v2)
    {
        return Mathf.RoundToInt(v1.x) == Mathf.RoundToInt(v2.x) 
			&& Mathf.RoundToInt(v1.y) == Mathf.RoundToInt(v2.y)
			&& Mathf.RoundToInt(v1.z) == Mathf.RoundToInt(v2.z); 
    }

    public int GetHashCode(Vector3 obj)
    {
		float x = Mathf.Round(obj.x);
		float y = Mathf.Round(obj.y);
		float z = Mathf.Round(obj.z);
		
        string xcomp = (x >= 0) ? "0" + x.ToString() : "1" + Mathf.Abs(x).ToString(); //0 Positivo o 0, 1 Negativo
        string ycomp = (y >= 0) ? "0" + y.ToString() : "1" + Mathf.Abs(y).ToString(); //0 Positivo o 0, 1 Negativo
        string zcomp = (z >= 0) ? "0" + z.ToString() : "1" + Mathf.Abs(z).ToString(); //0 Positivo o 0, 1 Negativo
        string hashCode = xcomp + ycomp + zcomp;
        return (int.Parse(hashCode));
    }
}
