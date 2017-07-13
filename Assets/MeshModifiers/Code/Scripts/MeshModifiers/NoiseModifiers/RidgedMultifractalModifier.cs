using UnityEngine;
using LibNoise.Generator;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Ridged Multifractal")]
public class RidgedMultifractalModifier : NoiseModifierBase
{
	[SerializeField]
	private RidgedMultifractal noiseModule = new RidgedMultifractal ();

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		if (!noiseModule.IsDisposed)
			noiseModule.Dispose ();

		var value = 0.5f + (float)noiseModule.GetValue (GetSampleCoordinate (basePosition));

		return FormatValue (value, basePosition);
	}
}
