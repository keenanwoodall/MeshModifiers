using UnityEngine;
using MeshModifiers;
using LibNoise.Generator;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Voronoi")]
public class VoronoiModifier : NoiseModifierBase
{
	[SerializeField]
	private Voronoi noiseModule = new Voronoi ();

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		if (!noiseModule.IsDisposed)
			noiseModule.Dispose ();

		var value = 0.5f + (float)noiseModule.GetValue (GetSampleCoordinate (vertexData.position));

		return FormatValue (value, vertexData);
	}
}
