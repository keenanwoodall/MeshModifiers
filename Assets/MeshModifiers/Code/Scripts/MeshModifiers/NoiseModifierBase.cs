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

		public Vector3
			speed = Vector3.one,
			magnitude = Vector3.one / 10;

		public float sampleScale = 1f;

		#endregion



		#region Main Methods

		protected Vector3 GetSampleCoordinate (Vector3 basePosition)
		{
			Vector3 coordinate = (basePosition + (speed * modObject.Time)) * sampleScale;
			if (useWorldPosition)
				coordinate += transform.position;

			return coordinate;
		}

		protected Vector3 FormatValue (float value, Vector3 basePosition)
		{
			if (spherical)
				return Vectorx.Multiply (basePosition, Vector3.one + (value * magnitude));
			else
				return basePosition + new Vector3 (value * magnitude.x, value * magnitude.y, value * magnitude.z) - magnitude;
		}

		protected Vector3 FormatValue (Vector3 value, Vector3 basePosition)
		{
			return Vectorx.Multiply (basePosition, Vector3.one + Vectorx.Multiply (value, magnitude));
		}

		public override void DrawEditorGUI ()
		{
			base.DrawEditorGUI ();

			useWorldPosition = GUILayout.Toggle (useWorldPosition, "Use World Position");
			spherical = GUILayout.Toggle (spherical, "Spherical");

			GUILayout.Label ("Animation Speed (x, y, z)");
			speed.x = GUILayout.HorizontalSlider (speed.x, 0f, 5f);
			speed.y = GUILayout.HorizontalSlider (speed.y, 0f, 5f);
			speed.z = GUILayout.HorizontalSlider (speed.z, 0f, 5f);

			GUILayout.Label ("Magnitude (x, y, z)");
			magnitude.x = GUILayout.HorizontalSlider (magnitude.x, 0f, 1f);
			magnitude.y = GUILayout.HorizontalSlider (magnitude.y, 0f, 1f);
			magnitude.z = GUILayout.HorizontalSlider (magnitude.z, 0f, 1f);

			GUILayout.Label ("Sample Scale");
			sampleScale = GUILayout.HorizontalSlider (sampleScale, 0.1f, 2f);
		}

		#endregion
	}
}