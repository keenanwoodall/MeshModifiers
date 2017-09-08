using UnityEngine;
using System.Collections;
using LibNoise;
using Mathx;

namespace MeshModifiers
{
	public abstract class NoiseModifierBase : MeshModifierBase
	{
		public bool
			useWorldPosition = false;
		public enum NoiseDirection { Local, Normal, Tangent, Spherical }
		public NoiseDirection noiseDirection = NoiseDirection.Spherical;

		public Vector3 offset = Vector3.zero;
		public Vector3 speed = Vector3.zero;

		public float 
			sampleScale = 1f,
			magnitudeMultiplier = 1f;

		[SerializeField]
		private Vector3 magnitude = Vector3.one / 10;
		public Vector3 Magnitude
		{
			get { return magnitude * magnitudeMultiplier; }
			set { magnitude = value; }
		}

		protected Vector3 GetSampleCoordinate (Vector3 basePosition)
		{
			Vector3 coordinate = (basePosition + (speed * modObject.Time) + offset) * sampleScale;
			if (useWorldPosition)
				coordinate += modObject.transform.position;

			return coordinate;
		}

		protected Vector3 FormatValue (float value, VertexData vertexData)
		{
			switch (noiseDirection)
			{
				case NoiseDirection.Local:
					return vertexData.position + (value * Magnitude) - Magnitude;
				case NoiseDirection.Normal:
					return vertexData.position + (Vector3.Scale (vertexData.normal, (value * Magnitude)));
				case NoiseDirection.Tangent:
					return vertexData.position + (Vector3.Scale (vertexData.tangent, (value * Magnitude)));
				default:
					return Vectorx.Multiply (vertexData.position, Vector3.one + (value * Magnitude));
			}
		}

		protected Vector3 FormatValue (Vector3 value, Vector3 basePosition)
		{
			return Vectorx.Multiply (basePosition, Vector3.one + Vectorx.Multiply (value, Magnitude));
		}
	}
}