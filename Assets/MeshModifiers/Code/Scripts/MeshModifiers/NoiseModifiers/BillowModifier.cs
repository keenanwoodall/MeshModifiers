using UnityEngine;
using MeshModifiers;
using LibNoise.Generator;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Noise/Billow")]
public class BillowModifier : NoiseModifierBase
{
	#region Private Properties

	[SerializeField]
	private Billow noiseModule = new Billow ();

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
