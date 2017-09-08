using UnityEngine;
using MeshModifiers;
using LibNoise.Generator;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Billow")]
public class BillowModifier : NoiseModifierBase
{
	[SerializeField]
	private Billow noiseModule = new Billow ();

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		if (!noiseModule.IsDisposed)
			noiseModule.Dispose ();

		var value = 0.5f + (float)noiseModule.GetValue (GetSampleCoordinate (vertexData.position));

		return FormatValue (value, vertexData);
	}
}
