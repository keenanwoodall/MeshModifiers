using UnityEngine;
using LibNoise.Generator;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Ridged Multifractal")]
public class RidgedMultifractalModifier : NoiseModifierBase
{
	#region Private Properties

	[SerializeField]
	private RidgedMultifractal noiseModule = new RidgedMultifractal ();

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
