using UnityEngine;
using System;
using System.Collections;
using MeshModifiers;
using DFNoise;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Divergence Free")]
public class DivergenceFreeModifier : NoiseModifierBase
{
	#region Public Properties

	public DFNoise3D noiseModule;

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		Vector3 sampleCoordinates = GetSampleCoordinate (basePosition);
		Vector3 value = ((spherical) ? noiseModule.GetDFNoise (sampleCoordinates) : noiseModule.GetGradient (sampleCoordinates));
		return FormatValue (value, basePosition);
	}

	#endregion
}