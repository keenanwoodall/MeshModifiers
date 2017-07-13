using UnityEngine;
using System.Collections;
using LibNoise;
using Mathx;

namespace MeshModifiers
{
	public abstract class NoiseModifierBase : MeshModifierBase
	{
		public bool
			useWorldPosition = false,
			spherical = true;

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

		protected Vector3 FormatValue (float value, Vector3 basePosition)
		{
			if (spherical)
				return Vectorx.Multiply (basePosition, Vector3.one + (value * Magnitude));
			else
				return basePosition + (value * Magnitude) - Magnitude;
		}

		protected Vector3 FormatValue (Vector3 value, Vector3 basePosition)
		{
			return Vectorx.Multiply (basePosition, Vector3.one + Vectorx.Multiply (value, Magnitude));
		}
	}
}