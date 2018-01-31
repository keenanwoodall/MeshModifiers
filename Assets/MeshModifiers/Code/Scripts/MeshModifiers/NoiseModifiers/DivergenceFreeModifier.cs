using UnityEngine;
using System;
using System.Collections;
using MeshModifiers;
using DFNoise;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Divergence Free")]
public class DivergenceFreeModifier : NoiseModifierBase
{
	public DFNoise3D noiseModule;

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var sampleCoordinates = GetSampleCoordinate (vertexData.position);
		var value = ((noiseDirection == NoiseDirection.Spherical) ? noiseModule.GetDFNoise (sampleCoordinates) : noiseModule.GetGradient (sampleCoordinates));
		
		return FormatValue (value, vertexData);
	}
}