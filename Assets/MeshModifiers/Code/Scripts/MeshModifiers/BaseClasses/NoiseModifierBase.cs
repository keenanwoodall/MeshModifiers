/*
* Copyright (c) 2016 Keenan Woodall
*
* This software is provided 'as-is', without any express or implied
* warranty. In no event will the authors be held liable for any damages
* arising from the use of this software.
*
* Permission is granted to anyone to use this software for any purpose,
* including commercial applications, and to alter it and redistribute it
* freely, subject to the following restrictions:
* 
*    1. The origin of this software must not be misrepresented; you must not
*    claim that you wrote the original software. If you use this software
*    in a product, an acknowledgment in the product documentation would be
*    appreciated but is not required.
* 
*    2. Altered source versions must be plainly marked as such, and must not be
*    misrepresented as being the original software.
* 
*    3. This notice may not be removed or altered from any source
*    distribution.
*/

using UnityEngine;
using System.Collections;
using LibNoise;
using Mathx;

namespace MeshModifiers
{
	public abstract class NoiseModifierBase : MeshModifierBase
	{
		#region Public Properties

		public bool
			useWorldPosition = false,
			spherical = true;

		public Vector3 offset = Vector3.zero;
		public Vector3 speed = Vector3.one;

		public float sampleScale = 1f;

		public float magnitudeMultiplier = 1f;

		#endregion



		#region Backed Properties

		[SerializeField]
		private Vector3 magnitude = Vector3.one / 10;
		public Vector3 Magnitude
		{
			get { return magnitude * magnitudeMultiplier; }
			set { magnitude = value; }
		}

		#endregion



		#region Main Methods

		protected Vector3 GetSampleCoordinate (Vector3 basePosition)
		{
			Vector3 coordinate = (basePosition + (speed * modObject.Time) + offset) * sampleScale;
			if (useWorldPosition)
				coordinate += modObject._transform.position;

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

		#endregion
	}
}