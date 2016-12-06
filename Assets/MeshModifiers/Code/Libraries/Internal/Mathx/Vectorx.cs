using UnityEngine;
using System.Collections;

namespace Mathx
{
	public static class Vectorx
	{
		public static Vector3 Clamp (Vector3 thisVector, Vector3 min, Vector3 max)
		{
			float x, y, z;

			x = Mathf.Clamp (thisVector.x, min.x, max.x);
			y = Mathf.Clamp (thisVector.y, min.y, max.y);
			z = Mathf.Clamp (thisVector.z, min.z, max.z);

			return new Vector3 (x, y, z);
		}

		public static Vector3 Multiply(Vector3 thisVector, Vector3 vector)
		{
			float x, y, z;
			x = thisVector.x * vector.x;
			y = thisVector.y * vector.y;
			z = thisVector.z * vector.z;
			return new Vector3(x, y, z);
		}

		public static Vector3 Divide(Vector3 thisVector, Vector3 vector)
		{
			float x, y, z;
			x = thisVector.x / vector.x;
			y = thisVector.y / vector.y;
			z = thisVector.z / vector.z;
			return new Vector3(x, y, z);
		}
	}
}