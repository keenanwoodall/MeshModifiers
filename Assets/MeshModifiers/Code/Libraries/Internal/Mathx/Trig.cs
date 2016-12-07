using UnityEngine;
using System;
using System.Collections;
using Mathx;

namespace Mathx.Trig
{
	public enum TrigType { Sin, Cos, Tan }

	public static class Trig3
	{
		public static Vector3 Sin3 (Vector3 t)
		{
			return new Vector3 (Mathf.Sin (t.x), Mathf.Sin (t.y), Mathf.Sin (t.z));
		}
		public static Vector3 Cos3 (Vector3 t)
		{
			return new Vector3 (Mathf.Cos (t.x), Mathf.Cos (t.y), Mathf.Cos (t.z));
		}
		public static Vector3 Tan3 (Vector3 t)
		{
			return new Vector3 (Mathf.Tan (t.x), Mathf.Tan (t.y), Mathf.Tan (t.z));
		}
	}

	[Serializable]
	public abstract class TrigObject
	{
		#region Public Properties

		public float
			amplitude = 0.1f,
			frequency = 0.5f;

		#endregion



		#region Private Properties

		private float
			phase,
			oldFrequency;

		#endregion



		#region Constructors

		public TrigObject ()
		{
			oldFrequency = frequency;
		}

		public TrigObject (float amplitude, float frequency)
		{
			this.amplitude = amplitude;
			this.frequency = frequency;
			oldFrequency = frequency;
		}

		#endregion



		#region Main Methods

		public float Solve (float t, float offset = 0f)
		{
			if (frequency != oldFrequency)
				CalculateNewFrequency (t);

			return TrigFunction (t * oldFrequency + phase + offset) * amplitude;
		}

		#endregion



		#region Utility Methods

		protected abstract float TrigFunction (float t);

		void CalculateNewFrequency (float t)
		{
			float current = (t * oldFrequency + phase) % (2f * Mathf.PI);
			float next = (t * frequency) % (2f * Mathf.PI);
			phase = current - next;
			oldFrequency = frequency;
		}

		#endregion
	}

	[Serializable]
	public class Sin : TrigObject
	{
		protected override float TrigFunction (float t)
		{
			return Mathf.Sin (t);
		}
	}

	[Serializable]
	public class Cos : TrigObject
	{
		protected override float TrigFunction (float t)
		{
			return Mathf.Cos (t);
		}
	}

	[Serializable]
	public class Tan : TrigObject
	{
		protected override float TrigFunction (float t)
		{
			return Mathf.Tan (t);
		}
	}

	[Serializable]
	public class Trig : TrigObject
	{
		public TrigType trigType = TrigType.Sin;

		public Trig (TrigType trigType)
		{
			this.trigType = trigType;
		}

		protected override float TrigFunction (float t)
		{
			switch (trigType)
			{
				case TrigType.Sin:
					return Mathf.Sin (t);
				case TrigType.Cos:
					return Mathf.Cos (t);
				case TrigType.Tan:
					return Mathf.Tan (t);
				default:
					Debug.Log ("TrigType is not Sin, Cos or Tan. Using Sin.");
					return Mathf.Sin (t);
			}
		}
	}
}