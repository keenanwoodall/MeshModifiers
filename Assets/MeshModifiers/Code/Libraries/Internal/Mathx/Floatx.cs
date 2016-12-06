using UnityEngine;
using System.Collections;

namespace Mathx
{
	public static class Floatx
	{
		public static void Remap(this float value, float min1, float max1, float min2, float max2)
		{
			if(max1 - min1 == 0)
				value =  (value - min1) * (max2 - min2) / Mathf.Epsilon;
			else
				value = (value - min1) * (max2 - min2) / (max1 - min1);
		}
		public static void Clamp(this float[] array, float min, float max)
		{
			for(int x = 0; x < array.GetLength(0) - 1; x++)
					array[x] = Mathf.Clamp(array[x], min, max);
		}
		public static void Clamp(this float[,] array, float min, float max)
		{
			for(int x = 0; x < array.GetLength(0) - 1; x++)
				for(int y = 0; y < array.GetLength(1) - 1; y++)
						array[x, y] = Mathf.Clamp(array[x, y], min, max);
		}
		public static void Clamp(this float[,,] array, float min, float max)
		{
			for(int x = 0; x < array.GetLength(0) - 1; x++)
				for(int y = 0; y < array.GetLength(1) - 1; y++)
					for(int z = 0; z < array.GetLength(2) - 1; z++)
						array[x, y, z] = Mathf.Clamp(array[x, y, z], min, max);
		}
		public static Vector3 ToVector3(this float f)
		{
			return new Vector3(f, f, f);
		}
	}
}