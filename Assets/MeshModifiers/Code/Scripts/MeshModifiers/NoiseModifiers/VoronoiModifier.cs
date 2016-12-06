using UnityEngine;
using MeshModifiers;
using LibNoise.Generator;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Voronoi")]
public class VoronoiModifier : NoiseModifierBase
{
	#region Private Properties

	[SerializeField]
	private Voronoi noiseModule = new Voronoi ();

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		if (!noiseModule.IsDisposed)
			noiseModule.Dispose ();

		float value = 0.5f + (float)noiseModule.GetValue (GetSampleCoordinate (basePosition));

		return FormatValue (value, basePosition);
	}

	#endregion
}
