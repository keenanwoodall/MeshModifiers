using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Scale Offset")]
public class ScaleOffsetModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3 offset = Vector3.one;

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		return Mathx.Vectorx.Multiply (basePosition, offset);
	}

	#endregion
}
