using UnityEngine;
using LibNoise.Generator;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Perlin")]
public class PerlinModifier : NoiseModifierBase
{
	[SerializeField]
	private Perlin noiseModule = new Perlin ();

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		if (!noiseModule.IsDisposed)
			noiseModule.Dispose ();
		
		var value = 0.5f + (float)noiseModule.GetValue (GetSampleCoordinate (vertexData.position));

		return FormatValue (value, vertexData.position);
	}
}
